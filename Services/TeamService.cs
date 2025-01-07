using LiveScore.Data;
using LiveScore.Models;
using Microsoft.EntityFrameworkCore;

public class TeamService
{
    private readonly LiveScoreContext _context;

    public TeamService(LiveScoreContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Team>> GetAllTeamsAsync()
    {
        return await _context.Teams.Include(t => t.Coach).ToListAsync();
    }

    public async Task<Team> GetTeamByIdAsync(int id)
    {
        return await _context.Teams.Include(t => t.Coach).FirstOrDefaultAsync(t => t.TeamId == id);
    }

    public async Task<Team> CreateTeamAsync(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }

    public async Task<bool> UpdateTeamAsync(Team team)
    {
        _context.Entry(team).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public async Task<bool> DeleteTeamAsync(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team == null) return false;

        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();
        return true;
    }

    public bool TeamExists(int id)
    {
        return _context.Teams.Any(t => t.TeamId == id);
    }
}
