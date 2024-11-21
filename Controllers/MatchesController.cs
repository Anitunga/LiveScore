using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchRepository _matchRepository;

        public MatchesController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            var matches = await _matchRepository.GetAllAsync();
            return Ok(matches);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _matchRepository.GetMatchWithDetailsAsync(id);
            if (match == null) return NotFound();
            return Ok(match);
        }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatchesByTeam(int teamId)
        {
            var matches = await _matchRepository.GetMatchesByTeamAsync(teamId);
            return Ok(matches);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatchesByUser(int userId)
        {
            var matches = await _matchRepository.GetMatchesByUserAsync(userId);
            return Ok(matches);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Match>> CreateMatch(Match match)
        {
            var createdMatch = await _matchRepository.AddAsync(match);
            return CreatedAtAction(nameof(GetMatch), new { id = createdMatch.MatchId }, createdMatch);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMatch(int id, Match match)
        {
            if (id != match.MatchId) return BadRequest();
            await _matchRepository.UpdateAsync(match);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            await _matchRepository.DeleteAsync(id);
            return NoContent();
        }
    }
} 