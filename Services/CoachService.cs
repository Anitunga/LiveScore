using LiveScore.Data;
using LiveScore.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveScore.Services
{
    public interface ICoachService
    {
        Task<IEnumerable<Coach>> GetAllCoachesAsync();
        Task<Coach> GetCoachByIdAsync(int id);
        Task<Coach> AddCoachAsync(Coach coach);
        Task<Coach> UpdateCoachAsync(int id, Coach coach);
        Task DeleteCoachAsync(int id);
    }

    public class CoachService : ICoachService
    {
        private readonly LiveScoreContext _context;

        public CoachService(LiveScoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Coach>> GetAllCoachesAsync() => await _context.Coaches.ToListAsync();

        public async Task<Coach> GetCoachByIdAsync(int id) => await _context.Coaches.FindAsync(id);

        public async Task<Coach> AddCoachAsync(Coach coach)
        {
            _context.Coaches.Add(coach);
            await _context.SaveChangesAsync();
            return coach;
        }

        public async Task<Coach> UpdateCoachAsync(int id, Coach coach)
        {
            var existingCoach = await _context.Coaches.FindAsync(id);
            if (existingCoach == null) throw new KeyNotFoundException($"Coach with ID {id} not found.");

            existingCoach.Name = coach.Name;
            existingCoach.Role = coach.Role;

            await _context.SaveChangesAsync();
            return existingCoach;
        }

        public async Task DeleteCoachAsync(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null) throw new KeyNotFoundException($"Coach with ID {id} not found.");

            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();
        }
    }
}
