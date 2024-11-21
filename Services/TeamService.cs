public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;

    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Team> GetByIdAsync(int id)
    {
        return await _teamRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Team>> GetAllAsync()
    {
        return await _teamRepository.GetAllAsync();
    }

    public async Task<Team> CreateAsync(Team team)
    {
        // Add validation logic here
        return await _teamRepository.AddAsync(team);
    }

    public async Task UpdateAsync(Team team)
    {
        // Add validation logic here
        await _teamRepository.UpdateAsync(team);
    }

    public async Task DeleteAsync(int id)
    {
        await _teamRepository.DeleteAsync(id);
    }

    public async Task<Team> GetTeamWithPlayersAsync(int teamId)
    {
        return await _teamRepository.GetTeamWithPlayersAsync(teamId);
    }

    public async Task<Team> GetTeamWithFullDetailsAsync(int teamId)
    {
        return await _teamRepository.GetTeamWithFullDetailsAsync(teamId);
    }
} 