using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LiveScore.Models;
using LiveScore.Repositories.Interfaces;

namespace LiveScore.Repositories
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(LiveScoreContext context) : base(context) { }

        public async Task<IEnumerable<Player>> GetPlayersByTeamAsync(int teamId)
        {
            return await _dbSet
                .Where(p => p.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<Player> GetPlayerWithTeamAsync(int playerId)
        {
            return await _dbSet
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.PlayerId == playerId);
        }
    }
} 