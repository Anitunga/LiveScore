using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LiveScore.Core.Entities;
using LiveScore.Core.Interfaces;
using LiveScore.Infrastructure.Data;

namespace LiveScore.Infrastructure.Repositories
{
    public class QuarterRepository : GenericRepository<Quarter>, IQuarterRepository
    {
        public QuarterRepository(LiveScoreContext context) : base(context) { }

        public async Task<IEnumerable<Quarter>> GetQuartersByMatchAsync(int matchId)
        {
            return await _dbSet
                .Include(q => q.Events)
                .Where(q => q.Events.Any(e => e.Quarter.QuarterId == matchId))
                .ToListAsync();
        }

        public async Task<Quarter> GetQuarterWithEventsAsync(int quarterId)
        {
            return await _dbSet
                .Include(q => q.Events)
                .FirstOrDefaultAsync(q => q.QuarterId == quarterId);
        }
    }
} 