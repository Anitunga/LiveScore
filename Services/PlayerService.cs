using LiveScore.Data;
using LiveScore.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveScore.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetPlayersAsync();
        Task<Player?> GetPlayerByIdAsync(int id);
        Task<Player> AddPlayerAsync(Player player);
        Task<bool> UpdatePlayerAsync(int id, Player player);
        Task<bool> DeletePlayerAsync(int id);
        Task<bool> PlayerExistsAsync(int id);
    }

    public class PlayerService : IPlayerService
    {
        private readonly LiveScoreContext _context;

        public PlayerService(LiveScoreContext context)
        {
            _context = context;
        }

        // Get All players
        public async Task<IEnumerable<Player>> GetPlayersAsync()
        {
            return await _context.Players
                .Include(p => p.Team)
                .ToListAsync();
        }

        // Get player by ID
        public async Task<Player?> GetPlayerByIdAsync(int id)
        {
            return await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.PlayerId == id);
        }

        // Add a new player
        public async Task<Player> AddPlayerAsync(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        // Update an existing player (only Admin and Encoder roles)
        public async Task<bool> UpdatePlayerAsync(int id, Player player/*, string? currentUserRole*/)
        {
            /*if (currentUserRole != "Admin" && currentUserRole != "Encoder")
            {
                return false; // Unauthorized
            }*/

            var existingPlayer = await _context.Players.FindAsync(id);
            if (existingPlayer == null)
            {
                return false; // Not Found
            }

            // Update specific fields
            existingPlayer.Name = player.Name;
            existingPlayer.Position = player.Position;
            existingPlayer.TeamId = player.TeamId;
            // etc...

            _context.Entry(existingPlayer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PlayerExistsAsync(id))
                {
                    return false; // Not Found after concurrency exception
                }
                throw;
            }
        }

        // Delete a player
        public async Task<bool> DeletePlayerAsync(int id)
        {
            /*if (currentUserRole != "Admin")
            {
                return false; // Unauthorized
            }*/

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return false; // Not Found
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return true;
        }

        // Check if a player exists
        public async Task<bool> PlayerExistsAsync(int id)
        {
            return await _context.Players.AnyAsync(p => p.PlayerId == id);
        }
    }
}
