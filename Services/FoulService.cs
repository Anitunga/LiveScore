using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class FoulService : IFoulService
    {
        private readonly IFoulRepository _foulRepository;
        private readonly IPlayerRepository _playerRepository;
        private const int FOUL_OUT_LIMIT = 5; // Maximum fouls before player fouls out

        public FoulService(IFoulRepository foulRepository, IPlayerRepository playerRepository)
        {
            _foulRepository = foulRepository;
            _playerRepository = playerRepository;
        }

        public async Task<IEnumerable<Foul>> GetAllAsync()
        {
            return await _foulRepository.GetAllAsync();
        }

        public async Task<Foul> GetByIdAsync(int id)
        {
            var foul = await _foulRepository.GetByIdAsync(id);
            if (foul == null)
                throw new KeyNotFoundException($"Foul with ID {id} not found");
            return foul;
        }

        public async Task<Foul> CreateAsync(Foul foul)
        {
            // Validate player exists
            var player = await _playerRepository.GetByIdAsync(foul.PlayerId);
            if (player == null)
                throw new KeyNotFoundException($"Player with ID {foul.PlayerId} not found");

            // Validate foul type
            if (!Enum.IsDefined(typeof(FoulType), foul.FoulType))
                throw new ArgumentException("Invalid foul type");

            // Check if player has already fouled out
            if (await HasPlayerFouledOutAsync(foul.PlayerId))
                throw new InvalidOperationException($"Player {player.Name} has already fouled out");

            return await _foulRepository.AddAsync(foul);
        }

        public async Task UpdateAsync(Foul foul)
        {
            var existingFoul = await _foulRepository.GetByIdAsync(foul.FoulId);
            if (existingFoul == null)
                throw new KeyNotFoundException($"Foul with ID {foul.FoulId} not found");

            // Validate player exists
            var player = await _playerRepository.GetByIdAsync(foul.PlayerId);
            if (player == null)
                throw new KeyNotFoundException($"Player with ID {foul.PlayerId} not found");

            // Validate foul type
            if (!Enum.IsDefined(typeof(FoulType), foul.FoulType))
                throw new ArgumentException("Invalid foul type");

            await _foulRepository.UpdateAsync(foul);
        }

        public async Task DeleteAsync(int id)
        {
            var foul = await _foulRepository.GetByIdAsync(id);
            if (foul == null)
                throw new KeyNotFoundException($"Foul with ID {id} not found");

            await _foulRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Foul>> GetFoulsByPlayerAsync(int playerId)
        {
            // Validate player exists
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player == null)
                throw new KeyNotFoundException($"Player with ID {playerId} not found");

            return await _foulRepository.GetFoulsByPlayerAsync(playerId);
        }

        public async Task<IEnumerable<Foul>> GetFoulsByQuarterAsync(int quarterId)
        {
            return await _foulRepository.GetFoulsByQuarterAsync(quarterId);
        }

        public async Task<int> GetPlayerFoulCountAsync(int playerId)
        {
            // Validate player exists
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player == null)
                throw new KeyNotFoundException($"Player with ID {playerId} not found");

            return await _foulRepository.GetPlayerFoulCountAsync(playerId);
        }

        public async Task<bool> HasPlayerFouledOutAsync(int playerId)
        {
            // Validate player exists
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player == null)
                throw new KeyNotFoundException($"Player with ID {playerId} not found");

            return await _foulRepository.HasPlayerFouledOutAsync(playerId);
        }

        public async Task<IEnumerable<Foul>> GetFoulsByTeamInQuarterAsync(int teamId, int quarterId)
        {
            return await _foulRepository.GetFoulsByTeamInQuarterAsync(teamId, quarterId);
        }
    }
} 