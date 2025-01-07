using LiveScore.Data;
using LiveScore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LiveScore.Services
{
    public class UserService
    {
        private readonly LiveScoreContext _context;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;

        public UserService(LiveScoreContext context, IConfiguration configuration)
        {
            _context = context;
            _jwtKey = configuration["Jwt:Key"];
            _jwtIssuer = configuration["Jwt:Issuer"];
            _jwtAudience = configuration["Jwt:Audience"];
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            if (await IsUsernameTakenAsync(user.Username))
            {
                return false;
            }

            user.Password = HashPassword(user.Password); //Hash the Password
            _context.Users.Add(user); //add the user in the DB
            await _context.SaveChangesAsync(); //Store the changes
            return true;
        }

        public async Task<string?> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !VerifyPassword(password, user.Password))
            {
                return null;
            }

            return GenerateJwtToken(user); //if user exist in DB, then generate a token
        }

        public async Task<bool> UpdateUserAsync(int id, User user)
        {
            /*if (currentUserRole != "Admin")
            {
                return false;
            }*/

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.Username = user.Username; //update the users's username
            if (!string.IsNullOrEmpty(user.Password))
            {
                existingUser.Password = HashPassword(user.Password);
            }
            existingUser.Role = user.Role; //update the role

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            //Only the Admin can remove an user
            /*if (currentUserRole != "Admin")
            {
                return false;
            }*/

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        #region GENERATE TOKEN
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _jwtIssuer,
                Audience = _jwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
    }
}
