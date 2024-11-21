public interface IEventRepository : IGenericRepository<Event>
{
    Task<IEnumerable<Event>> GetEventsByQuarterAsync(int quarterId);
    Task<IEnumerable<Event>> GetEventsByPlayerAsync(int playerId);
} 