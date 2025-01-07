using LiveScore.Models;
using LiveScore.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuarterController : ControllerBase
    {
        private readonly QuarterService _quarterService;

        public QuarterController(QuarterService quarterService)
        {
            _quarterService = quarterService;
        }

        // GET: api/Quarter
        [HttpGet]
        public async Task<ActionResult<List<Quarter>>> GetQuarters()
        {
            var quarters = await _quarterService.GetAllQuartersAsync();
            return Ok(quarters);
        }

        // GET: api/Quarter/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Quarter>> GetQuarter(int id)
        {
            var quarter = await _quarterService.GetQuarterByIdAsync(id);
            if (quarter == null) return NotFound();
            return Ok(quarter);
        }

        // POST: api/Quarter
        [HttpPost]
        public async Task<ActionResult<Quarter>> CreateQuarter(Quarter quarter)
        {
            var createdQuarter = await _quarterService.AddQuarterAsync(quarter);
            return CreatedAtAction(nameof(GetQuarter), new { id = createdQuarter.QuarterId }, new
            {
                Message = "Quarter added successfully",
                Quarter = createdQuarter
            });
        }
    }
}
