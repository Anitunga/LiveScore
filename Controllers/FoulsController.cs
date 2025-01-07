using LiveScore.Models;
using LiveScore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiveScore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoulsController : ControllerBase
    {
        private readonly FoulService _foulService;

        public FoulsController(FoulService foulService)
        {
            _foulService = foulService;
        }

        // GET: api/Foul
        [HttpGet]
        public async Task<IActionResult> GetFouls()
        {
            var fouls = await _foulService.GetAllFoulsAsync();
            return Ok(fouls);
        }

        // GET: api/Foul/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Foul>> GetFoul(int id)
        {
            var foul = await _foulService.GetFoulByIdAsync(id);
            if (foul == null) return NotFound();
            return Ok(foul);
        }

        // Post: api/Foul
        [HttpPost]
        public async Task<ActionResult<Foul>> CreateFoul(Foul foul)
        {
            var createdFoul = await _foulService.AddFoulAsync(foul);
            return CreatedAtAction(nameof(GetFoul), new { id = createdFoul.FoulId }, new
            {
                Message = "Foul added successfully",
                Foul = createdFoul
            });
        }
    }
}
