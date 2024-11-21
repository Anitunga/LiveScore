using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LiveScore.Models;
using LiveScore.Repositories.Interfaces;

namespace LiveScore.Repositories
{
    public class MatchRepository : GenericRepository<Match>, IMatchRepository
    {
        public MatchRepository(LiveScoreContext context) : base(context) { }

        public async Task<IEnumerable<Match>> GetMatchesByTeamAsync(int teamId)
        {
            return await _dbSet
                .Include(m => m.TeamA)
                .Include(m => m.TeamB)
                .Include(m => m.Quarters)
                .Where(m => m.TeamAId == teamId || m.TeamBId == teamId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Match>> GetMatchesByUserAsync(int userId)
        {
            return await _dbSet
                .Include(m => m.TeamA)
                .Include(m => m.TeamB)
                .Include(m => m.Quarters)
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }

        public async Task<Match> GetMatchWithDetailsAsync(int matchId)
        {
            return await _dbSet
                .Include(m => m.TeamA)
                .Include(m => m.TeamB)
                .Include(m => m.Quarters)
                    .ThenInclude(q => q.Events)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MatchId == matchId);
        }
    }
} 