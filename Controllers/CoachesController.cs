using LiveScore.Models;
using LiveScore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiveScore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController : ControllerBase
    {
        private readonly CoachService _coachService;

        public CoachesController(CoachService coachService)
        {
            _coachService = coachService;
        }

        // GET: api/Coach
        [HttpGet]
        public async Task<IActionResult> GetAllCoaches()
        {
            var coaches = await _coachService.GetAllCoachesAsync();
            return Ok(coaches);
        }

        // GET: api/Coach/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoachById(int id)
        {
            var coach = await _coachService.GetCoachByIdAsync(id);
            if (coach == null) return NotFound();
            return Ok(coach);
        }

        // POST: api/Coach
        [HttpPost]
        public async Task<IActionResult> AddCoach(Coach coach)
        {
            var createdCoach = await _coachService.AddCoachAsync(coach);
            return CreatedAtAction(nameof(GetCoachById), new { id = createdCoach.CoachId }, new
            {
                Message = "Coach added successfully",
                Coach = createdCoach
            });
        }

        // PUT: api/Coach/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoach(int id, Coach coach)
        {
            try
            {
                var updatedCoach = await _coachService.UpdateCoachAsync(id, coach);
                return Ok(updatedCoach);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Coach/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            try
            {
                await _coachService.DeleteCoachAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
