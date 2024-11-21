public interface IFoulService
{
    Task<IEnumerable<Foul>> GetAllAsync();
    Task<Foul> GetByIdAsync(int id);
    Task<Foul> CreateAsync(Foul foul);
    Task UpdateAsync(Foul foul);
    Task DeleteAsync(int id);
    Task<IEnumerable<Foul>> GetFoulsByPlayerAsync(int playerId);
    Task<IEnumerable<Foul>> GetFoulsByQuarterAsync(int quarterId);
    Task<int> GetPlayerFoulCountAsync(int playerId);
    Task<bool> HasPlayerFouledOutAsync(int playerId);
    Task<IEnumerable<Foul>> GetFoulsByTeamInQuarterAsync(int teamId, int quarterId);
} 