public interface IMatchRepository : IGenericRepository<Match>
{
    Task<IEnumerable<Match>> GetMatchesByTeamAsync(int teamId);
    Task<IEnumerable<Match>> GetMatchesByUserAsync(int userId);
    Task<Match> GetMatchWithDetailsAsync(int matchId);
} 