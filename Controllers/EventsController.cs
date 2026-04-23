using eBooking.Data;
using eBooking.DTO;
using eBooking.Interfaces;
using eBooking.Wrappers;
using eBooking.Services;
using Microsoft.AspNetCore.Mvc;

namespace eBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventServices)
        {
            _eventService = eventServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventDTO eventDto)
        {
           var response = await _eventService.CreateAsync(eventDto);
            return Ok(response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _eventService.GetByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAllEvents( int page = 1, int limit = 10)
        {
            var response = await _eventService.GetAllAsync(page, limit);
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDTO eventDto)
        {
            var response = await _eventService.UpdateAsync(id, eventDto);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteAsync(id);
            return Ok();
        }
    }
}