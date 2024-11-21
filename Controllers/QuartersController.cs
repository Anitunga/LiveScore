using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuartersController : ControllerBase
    {
        private readonly IQuarterRepository _quarterRepository;

        public QuartersController(IQuarterRepository quarterRepository)
        {
            _quarterRepository = quarterRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quarter>>> GetQuarters()
        {
            var quarters = await _quarterRepository.GetAllAsync();
            return Ok(quarters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Quarter>> GetQuarter(int id)
        {
            var quarter = await _quarterRepository.GetQuarterWithEventsAsync(id);
            if (quarter == null) return NotFound();
            return Ok(quarter);
        }

        [HttpGet("match/{matchId}")]
        public async Task<ActionResult<IEnumerable<Quarter>>> GetQuartersByMatch(int matchId)
        {
            var quarters = await _quarterRepository.GetQuartersByMatchAsync(matchId);
            return Ok(quarters);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Encoder")]
        public async Task<ActionResult<Quarter>> CreateQuarter(Quarter quarter)
        {
            var createdQuarter = await _quarterRepository.AddAsync(quarter);
            return CreatedAtAction(nameof(GetQuarter), new { id = createdQuarter.QuarterId }, createdQuarter);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Encoder")]
        public async Task<IActionResult> UpdateQuarter(int id, Quarter quarter)
        {
            if (id != quarter.QuarterId) return BadRequest();
            await _quarterRepository.UpdateAsync(quarter);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteQuarter(int id)
        {
            await _quarterRepository.DeleteAsync(id);
            return NoContent();
        }
    }
} 