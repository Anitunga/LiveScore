using LiveScore.Models;
using LiveScore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiveScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        // GET: api/Users/5
        //[Authorize(Roles = "Admin, Encoder")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/Users/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var success = await _userService.RegisterUserAsync(user);
            if (!success)
            {
                return BadRequest(new { error = "Username already exists" });
            }

            return Ok(new { message = "User registered successfully" });
        }

        // POST: api/Users/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _userService.AuthenticateUserAsync(request.Username, request.Password);
            if (token == null)
            {
                return Unauthorized(new { error = "Invalid username or password" });
            }

            return Ok(new
            {
                message = "Login successful",
                token
            });
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            //var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var result = await _userService.UpdateUserAsync(id, user);

            if (!result)
            {
                return Forbid(); // 403 Forbidden or NotFound
            }

            return Ok(new { message = "User updated successfully" });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var result = await _userService.DeleteUserAsync(id);

            if (!result)
            {
                return Forbid(); // 403 Forbidden or NotFound
            }

            return Ok(new { message = "User deleted successfully" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
