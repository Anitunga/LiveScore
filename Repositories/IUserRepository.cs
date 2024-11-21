public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
} 