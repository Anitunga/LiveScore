public interface IMatchService
{
    Task<Match> GetByIdAsync(int id);
    Task<IEnumerable<Match>> GetAllAsync();
    Task<Match> CreateAsync(Match match);
    Task UpdateAsync(Match match);
    Task DeleteAsync(int id);
    Task<IEnumerable<Match>> GetMatchesByTeamAsync(int teamId);
    Task<IEnumerable<Match>> GetMatchesByUserAsync(int userId);
    Task<Match> GetMatchWithDetailsAsync(int matchId);
} 