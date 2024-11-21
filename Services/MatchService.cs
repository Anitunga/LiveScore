public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;

    public MatchService(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<Match> CreateAsync(Match match)
    {
        // Add validation logic here
        return await _matchRepository.AddAsync(match);
    }

    public async Task<Match> UpdateAsync(Match match)
    {
        // Add validation logic here
        await _matchRepository.UpdateAsync(match);
        return match;
    }

    public async Task DeleteAsync(int id)
    {
        await _matchRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Match>> GetMatchesByTeamAsync(int teamId)
    {
        return await _matchRepository.GetMatchesByTeamAsync(teamId);
    }

    public async Task<IEnumerable<Match>> GetMatchesByUserAsync(int userId)
    {
        return await _matchRepository.GetMatchesByUserAsync(userId);
    }

    public async Task<Match> GetMatchWithDetailsAsync(int matchId)
    {
        return await _matchRepository.GetMatchWithDetailsAsync(matchId);
    }
} 