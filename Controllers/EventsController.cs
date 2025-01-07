using LiveScore.Models;
using LiveScore.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        // GET: api/Event/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null) return NotFound();
            return Ok(eventItem);
        }

        // POST: api/Event
        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(Event eventItem)
        {
            var createdEvent = await _eventService.AddEventAsync(eventItem);
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.EventId }, new
            {
                Message = "Event added successfully",
                Event = createdEvent
            });
        }
    }
}
