public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Event> GetByIdAsync(int id)
    {
        return await _eventRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _eventRepository.GetAllAsync();
    }

    public async Task<Event> CreateAsync(Event @event)
    {
        // Add validation logic here
        return await _eventRepository.AddAsync(@event);
    }

    public async Task UpdateAsync(Event @event)
    {
        // Add validation logic here
        await _eventRepository.UpdateAsync(@event);
    }

    public async Task DeleteAsync(int id)
    {
        await _eventRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Event>> GetEventsByQuarterAsync(int quarterId)
    {
        return await _eventRepository.GetEventsByQuarterAsync(quarterId);
    }

    public async Task<IEnumerable<Event>> GetEventsByPlayerAsync(int playerId)
    {
        return await _eventRepository.GetEventsByPlayerAsync(playerId);
    }
} 