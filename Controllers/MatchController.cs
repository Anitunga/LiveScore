using LiveScore.Data;
using LiveScore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiveScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly LiveScoreContext _context;

        public MatchController(LiveScoreContext context)
        {
            _context = context;
        }

        // GET: api/Match
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            return await _context.Matches
                .Include(m => m.TeamA)
                .Include(m => m.TeamB)
                .Include(m => m.Quarters)
                .Include(m => m.User)
                .ToListAsync();
        }

        // GET: api/Match/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _context.Matches
                .Include(m => m.TeamA)
                .Include(m => m.TeamB)
                .Include(m => m.Quarters)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MatchId == id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // POST: api/Match
        [HttpPost]
        public async Task<IActionResult> AddMatch(Match match)
        {
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

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

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
                {
                    return NotFound(new { Message = "Match not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Match updated successfully" });
        }

        // DELETE: api/Match/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound(new { Message = "Match not found" });
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Match deleted successfully" });
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(m => m.MatchId == id);
        }
    }
}
