[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlayersController : ControllerBase
{
    private readonly IPlayerRepository _playerRepository;

    public PlayersController(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
    {
        var players = await _playerRepository.GetAllAsync();
        return Ok(players);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> GetPlayer(int id)
    {
        var player = await _playerRepository.GetPlayerWithTeamAsync(id);
        if (player == null) return NotFound();
        return Ok(player);
    }

    [HttpGet("team/{teamId}")]
    public async Task<ActionResult<IEnumerable<Player>>> GetPlayersByTeam(int teamId)
    {
        var players = await _playerRepository.GetPlayersByTeamAsync(teamId);
        return Ok(players);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Player>> CreatePlayer(Player player)
    {
        var createdPlayer = await _playerRepository.AddAsync(player);
        return CreatedAtAction(nameof(GetPlayer), new { id = createdPlayer.PlayerId }, createdPlayer);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePlayer(int id, Player player)
    {
        if (id != player.PlayerId) return BadRequest();
        await _playerRepository.UpdateAsync(player);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        await _playerRepository.DeleteAsync(id);
        return NoContent();
    }
} 