public interface IEventService
{
    Task<Event> GetByIdAsync(int id);
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event> CreateAsync(Event @event);
    Task UpdateAsync(Event @event);
    Task DeleteAsync(int id);
    Task<IEnumerable<Event>> GetEventsByQuarterAsync(int quarterId);
    Task<IEnumerable<Event>> GetEventsByPlayerAsync(int playerId);
} 