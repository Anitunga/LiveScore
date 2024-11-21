using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FoulsController : ControllerBase
    {
        private readonly IFoulRepository _foulRepository;

        public FoulsController(IFoulRepository foulRepository)
        {
            _foulRepository = foulRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Foul>>> GetFouls()
        {
            var fouls = await _foulRepository.GetAllAsync();
            return Ok(fouls);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Foul>> GetFoul(int id)
        {
            var foul = await _foulRepository.GetByIdAsync(id);
            if (foul == null) return NotFound();
            return Ok(foul);
        }

        [HttpGet("player/{playerId}")]
        public async Task<ActionResult<IEnumerable<Foul>>> GetFoulsByPlayer(int playerId)
        {
            var fouls = await _foulRepository.GetFoulsByPlayerAsync(playerId);
            return Ok(fouls);
        }

        [HttpGet("player/{playerId}/count")]
        public async Task<ActionResult<int>> GetPlayerFoulCount(int playerId)
        {
            var count = await _foulRepository.GetPlayerFoulCountAsync(playerId);
            return Ok(count);
        }

        [HttpGet("quarter/{quarterId}")]
        public async Task<ActionResult<IEnumerable<Foul>>> GetFoulsByQuarter(int quarterId)
        {
            var fouls = await _foulRepository.GetFoulsByQuarterAsync(quarterId);
            return Ok(fouls);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Encoder")]
        public async Task<ActionResult<Foul>> CreateFoul(Foul foul)
        {
            var createdFoul = await _foulRepository.AddAsync(foul);
            return CreatedAtAction(nameof(GetFoul), new { id = createdFoul.FoulId }, createdFoul);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Encoder")]
        public async Task<IActionResult> UpdateFoul(int id, Foul foul)
        {
            if (id != foul.FoulId) return BadRequest();
            await _foulRepository.UpdateAsync(foul);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFoul(int id)
        {
            await _foulRepository.DeleteAsync(id);
            return NoContent();
        }
    }
} 