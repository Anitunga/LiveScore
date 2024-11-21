using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LiveScore.Models;
using LiveScore.Repositories;

namespace LiveScore.Repositories
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(LiveScoreContext context) : base(context) { }

        public async Task<Team> GetTeamWithPlayersAsync(int teamId)
        {
            return await _dbSet
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.TeamId == teamId);
        }

        public async Task<Team> GetTeamWithCoachAsync(int teamId)
        {
            return await _dbSet
                .Include(t => t.Coach)
                .FirstOrDefaultAsync(t => t.TeamId == teamId);
        }

        public async Task<Team> GetTeamWithFullDetailsAsync(int teamId)
        {
            return await _dbSet
                .Include(t => t.Players)
                .Include(t => t.Coach)
                .FirstOrDefaultAsync(t => t.TeamId == teamId);
        }
    }
} 