public interface IFoulRepository : IGenericRepository<Foul>
{
    Task<IEnumerable<Foul>> GetFoulsByPlayerAsync(int playerId);
    Task<IEnumerable<Foul>> GetFoulsByQuarterAsync(int quarterId);
    Task<int> GetPlayerFoulCountAsync(int playerId);
} 