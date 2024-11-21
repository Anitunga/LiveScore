using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LiveScore.Models;
using LiveScore.Repositories.Interfaces;

namespace LiveScore.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(LiveScoreContext context) : base(context) { }

        public async Task<IEnumerable<Event>> GetEventsByQuarterAsync(int quarterId)
        {
            return await _dbSet
                .Include(e => e.PlayerInvolved)
                .Where(e => e.QuarterId == quarterId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByPlayerAsync(int playerId)
        {
            return await _dbSet
                .Include(e => e.Quarter)
                .Where(e => e.PlayerInvolved.PlayerId == playerId)
                .ToListAsync();
        }
    }
} 