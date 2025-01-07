using LiveScore.Data;
using LiveScore.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveScore.Services
{
    public class QuarterService
    {
        private readonly LiveScoreContext _context;

        public QuarterService(LiveScoreContext context)
        {
            _context = context;
        }

        public async Task<List<Quarter>> GetAllQuartersAsync()
        {
            return await _context.Quarters
                .Include(q => q.Events)
                .ToListAsync();
        }

        public async Task<Quarter?> GetQuarterByIdAsync(int id)
        {
            return await _context.Quarters
                .Include(q => q.Events)
                .FirstOrDefaultAsync(q => q.QuarterId == id);
        }

        public async Task<Quarter> AddQuarterAsync(Quarter quarter)
        {
            _context.Quarters.Add(quarter);
            await _context.SaveChangesAsync();
            return quarter;
        }
    }
}
