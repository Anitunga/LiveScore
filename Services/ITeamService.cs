public interface ITeamService
{
    Task<Team> GetByIdAsync(int id);
    Task<IEnumerable<Team>> GetAllAsync();
    Task<Team> CreateAsync(Team team);
    Task UpdateAsync(Team team);
    Task DeleteAsync(int id);
    Task<Team> GetTeamWithPlayersAsync(int teamId);
    Task<Team> GetTeamWithFullDetailsAsync(int teamId);
} 