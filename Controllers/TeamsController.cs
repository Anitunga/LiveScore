[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TeamsController : ControllerBase
{
    private readonly ITeamRepository _teamRepository;

    public TeamsController(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
    {
        var teams = await _teamRepository.GetAllAsync();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Team>> GetTeam(int id)
    {
        var team = await _teamRepository.GetTeamWithFullDetailsAsync(id);
        if (team == null) return NotFound();
        return Ok(team);
    }

    [HttpGet("{id}/players")]
    public async Task<ActionResult<Team>> GetTeamWithPlayers(int id)
    {
        var team = await _teamRepository.GetTeamWithPlayersAsync(id);
        if (team == null) return NotFound();
        return Ok(team);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Team>> CreateTeam(Team team)
    {
        var createdTeam = await _teamRepository.AddAsync(team);
        return CreatedAtAction(nameof(GetTeam), new { id = createdTeam.TeamId }, createdTeam);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateTeam(int id, Team team)
    {
        if (id != team.TeamId) return BadRequest();
        await _teamRepository.UpdateAsync(team);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTeam(int id)
    {
        await _teamRepository.DeleteAsync(id);
        return NoContent();
    }
} 