public interface IUserService
{
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> AuthenticateAsync(string username, string password);
    Task<bool> IsUsernameUniqueAsync(string username);
} 