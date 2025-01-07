using LiveScore.Models;
using LiveScore.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubstitutionController : ControllerBase
    {
        private readonly SubstitutionService _substitutionService;

        public SubstitutionController(SubstitutionService substitutionService)
        {
            _substitutionService = substitutionService;
        }

        // GET: api/Substitution/
        [HttpGet]
        public async Task<ActionResult<List<Substitution>>> GetSubstitutions()
        {
            var substitutions = await _substitutionService.GetAllSubstitutionsAsync();
            return Ok(substitutions);
        }

        // GET: api/Substitution/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Substitution>> GetSubstitution(int id)
        {
            var substitution = await _substitutionService.GetSubstitutionByIdAsync(id);
            if (substitution == null) return NotFound();
            return Ok(substitution);
        }

        // POST: api/Substitution/
        [HttpPost]
        public async Task<ActionResult<Substitution>> CreateSubstitution(Substitution substitution)
        {
            var createdSubstitution = await _substitutionService.AddSubstitutionAsync(substitution);
            return CreatedAtAction(nameof(GetSubstitution), new { id = createdSubstitution.SubstitutionId }, new
            {
                Message = "Substitution added successfully",
                Substitution = createdSubstitution
            });
        }
    }

}
