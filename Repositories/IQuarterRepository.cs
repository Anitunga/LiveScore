public interface IQuarterRepository : IGenericRepository<Quarter>
{
    Task<IEnumerable<Quarter>> GetQuartersByMatchAsync(int matchId);
    Task<Quarter> GetQuarterWithEventsAsync(int quarterId);
} 