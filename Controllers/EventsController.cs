using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _eventRepository.GetAllAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            if (@event == null) return NotFound();
            return Ok(@event);
        }

        [HttpGet("quarter/{quarterId}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByQuarter(int quarterId)
        {
            var events = await _eventRepository.GetEventsByQuarterAsync(quarterId);
            return Ok(events);
        }

        [HttpGet("player/{playerId}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByPlayer(int playerId)
        {
            var events = await _eventRepository.GetEventsByPlayerAsync(playerId);
            return Ok(events);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Encoder")]
        public async Task<ActionResult<Event>> CreateEvent(Event @event)
        {
            var createdEvent = await _eventRepository.AddAsync(@event);
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.EventId }, createdEvent);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Encoder")]
        public async Task<IActionResult> UpdateEvent(int id, Event @event)
        {
            if (id != @event.EventId) return BadRequest();
            await _eventRepository.UpdateAsync(@event);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventRepository.DeleteAsync(id);
            return NoContent();
        }
    }
} 