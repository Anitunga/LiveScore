using LiveScore.Data;
using LiveScore.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveScore.Services
{
    public class EventService
    {
        private readonly LiveScoreContext _context;

        public EventService(LiveScoreContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Quarter)
                .ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events
                .Include(e => e.Quarter)
                .FirstOrDefaultAsync(e => e.EventId == id);
        }

        public async Task<Event> AddEventAsync(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return ev;
        }
    }
}
