using LiveScore.Data;
using LiveScore.Models;
using Microsoft.EntityFrameworkCore;

public class MatchService
{
    private readonly LiveScoreContext _context;

    public MatchService(LiveScoreContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Match>> GetAllMatchesAsync()
    {
        return await _context.Matches
            .Include(m => m.TeamA)
            .Include(m => m.TeamB)
            .Include(m => m.Quarters)
            .Include(m => m.User)
            .ToListAsync();
    }

    public async Task<Match> GetMatchByIdAsync(int id)
    {
        return await _context.Matches
            .Include(m => m.TeamA)
            .Include(m => m.TeamB)
            .Include(m => m.Quarters)
            .Include(m => m.User)
            .FirstOrDefaultAsync(m => m.MatchId == id);
    }

    public async Task<Match> AddMatchAsync(Match match)
    {
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();
        return match;
    }

    public async Task<bool> UpdateMatchAsync(Match match)
    {
        _context.Entry(match).State = EntityState.Modified;

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

    public async Task<bool> DeleteMatchAsync(int id)
    {
        var match = await _context.Matches.FindAsync(id);
        if (match == null) return false;

        _context.Matches.Remove(match);
        await _context.SaveChangesAsync();
        return true;
    }

    public bool MatchExists(int id)
    {
        return _context.Matches.Any(m => m.MatchId == id);
    }
}
