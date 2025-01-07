using LiveScore.Data;
using LiveScore.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveScore.Services
{
    public class FoulService
    {
        private readonly LiveScoreContext _context;

        public FoulService(LiveScoreContext context)
        {
            _context = context;
        }

        public async Task<List<Foul>> GetAllFoulsAsync()
        {
            return await _context.Fouls
                .Include(f => f.Player)
                .ToListAsync();
        }

        public async Task<Foul?> GetFoulByIdAsync(int id)
        {
            return await _context.Fouls
                .Include(f => f.Player)
                .FirstOrDefaultAsync(f => f.FoulId == id);
        }

        public async Task<Foul> AddFoulAsync(Foul foul)
        {
            _context.Fouls.Add(foul);
            await _context.SaveChangesAsync();
            return foul;
        }
    }
}
