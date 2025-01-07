using LiveScore.Data;
using LiveScore.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveScore.Services
{
    public class SubstitutionService
    {
        private readonly LiveScoreContext _context;

        public SubstitutionService(LiveScoreContext context)
        {
            _context = context;
        }

        public async Task<List<Substitution>> GetAllSubstitutionsAsync()
        {
            return await _context.Substitutions
                .Include(s => s.PlayerIn)
                .Include(s => s.PlayerOut)
                .ToListAsync();
        }

        public async Task<Substitution?> GetSubstitutionByIdAsync(int id)
        {
            return await _context.Substitutions
                .Include(s => s.PlayerIn)
                .Include(s => s.PlayerOut)
                .FirstOrDefaultAsync(s => s.SubstitutionId == id);
        }

        public async Task<Substitution> AddSubstitutionAsync(Substitution substitution)
        {
            _context.Substitutions.Add(substitution);
            await _context.SaveChangesAsync();
            return substitution;
        }
    }
}
