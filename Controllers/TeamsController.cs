using LiveScore.Data;
using LiveScore.Models;
using LiveScore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiveScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamService _teamService;

        public TeamsController(TeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);

        }

        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(Team team)
        {
            var createdTeam = await _teamService.CreateTeamAsync(team);
            return CreatedAtAction(nameof(GetTeam), new { id = createdTeam.TeamId }, new
            {
                Message = "Team created successfully",
                Team = createdTeam
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTeam(int id, Team team)
        {
            if (id != team.TeamId)
            {
                return BadRequest();
            }

            try
            {
                await _teamService.UpdateTeamAsync(team);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return Ok(new { message = "Team updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                await _teamService.DeleteTeamAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return Ok(new { message = "Team deleted successfully" });
        }
    }
}