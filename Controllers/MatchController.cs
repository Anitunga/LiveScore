using LiveScore.Data;
using LiveScore.Models;
using LiveScore.Pages;
using LiveScore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiveScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly MatchService _matchService;

        public MatchController(MatchService matchService)
        {
            _matchService = matchService;
        }

        // GET: api/Match
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            var matches = await _matchService.GetAllMatchesAsync();
            return Ok(matches);

        }

        // GET: api/Match/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        // POST: api/Match
        [HttpPost]
        public async Task<IActionResult> AddMatch(Match match)
        {
            var createdMatch = await _matchService.AddMatchAsync(match);
            return Ok(new
            {
                Message = "Match created successfully",
                Match = match
            });
        }

        // PUT: api/Match/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditMatch(int id, Match match)
        {
            if (id != match.MatchId)
            {
                return BadRequest(new { Message = "Match ID mismatch" });
            }

            try
            {
                await _matchService.UpdateMatchAsync(match);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Match not found" });
            }

            return Ok(new { Message = "Match updated successfully" });
        }

        // DELETE: api/Match/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            try
            {
                await _matchService.DeleteMatchAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Match not found" });
            }

            return Ok(new { Message = "Match deleted successfully" });
        }
    }
}
