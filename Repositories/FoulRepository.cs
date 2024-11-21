using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LiveScore.Data;
using LiveScore.Models;

namespace LiveScore.Repositories
{
    public class FoulRepository : GenericRepository<Foul>, IFoulRepository
    {
        public FoulRepository(LiveScoreContext context) : base(context) { }

        public async Task<IEnumerable<Foul>> GetFoulsByPlayerAsync(int playerId)
        {
            return await _dbSet
                .Include(f => f.Quarter)
                .Where(f => f.PlayerId == playerId)
                .OrderBy(f => f.Quarter.Number)
                .ThenBy(f => f.Time)
                .ToListAsync();
        }

        public async Task<IEnumerable<Foul>> GetFoulsByQuarterAsync(int quarterId)
        {
            return await _dbSet
                .Include(f => f.Player)
                .Where(f => f.Quarter.QuarterId == quarterId)
                .OrderBy(f => f.Time)
                .ToListAsync();
        }

        public async Task<int> GetPlayerFoulCountAsync(int playerId)
        {
            return await _dbSet
                .CountAsync(f => f.PlayerId == playerId);
        }
    }
} 