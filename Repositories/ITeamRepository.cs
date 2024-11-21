public interface ITeamRepository : IGenericRepository<Team>
{
    Task<Team> GetTeamWithPlayersAsync(int teamId);
    Task<Team> GetTeamWithCoachAsync(int teamId);
    Task<Team> GetTeamWithFullDetailsAsync(int teamId);
} 