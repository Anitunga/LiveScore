public interface IPlayerRepository : IGenericRepository<Player>
{
    Task<IEnumerable<Player>> GetPlayersByTeamAsync(int teamId);
    Task<Player> GetPlayerWithTeamAsync(int playerId);
} 