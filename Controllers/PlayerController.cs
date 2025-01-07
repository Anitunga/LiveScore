using LiveScore.Data;
using LiveScore.Models;
using LiveScore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiveScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly LiveScoreContext _context;
        private readonly PlayerService _playerService;

        public PlayerController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: api/Player
        [HttpGet]
        // [Authorize(Roles = "Viewer,Encoder,Admin")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            var players = await _playerService.GetPlayersAsync();
            return Ok(players);
        }

        // GET: api/Player/{id}
        [HttpGet("{id}")]
        //[Authorize(Roles = "Viewer,Encoder,Admin")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // POST: api/Player
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<Player>> AddPlayer([FromBody] Player player)
        {
            var createdPlayer = await _playerService.AddPlayerAsync(player);
            return CreatedAtAction(nameof(GetPlayer), new { id = createdPlayer.PlayerId }, new
            {
                Message = "Player created successfully",
                Player = createdPlayer
            });
        }

        // PUT: api/Player/{id}
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Encoder")]
        public async Task<IActionResult> EditPlayer(int id, [FromBody] Player player)
        {
            //var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var result = await _playerService.UpdatePlayerAsync(id, player);

            if (!result)
            {
                return Forbid(); // 403 Forbidden if unauthorized or not found
            }

            return Ok(new { message = "Player updated successfully" });
        }

        // DELETE: api/Player/{id}
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            //var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var result = await _playerService.DeletePlayerAsync(id);

            if (!result)
            {
                return Forbid(); // 403 Forbidden if unauthorized or not found
            }

            return Ok(new { message = "Player deleted successfully" });
        }
    }
}
