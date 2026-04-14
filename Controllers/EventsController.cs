using eBooking.Data;
using eBooking.DTO;
using eBooking.Interfaces;
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
           var response = await _eventService.CreateEventAsync(eventDto);
            return Ok(response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var response = await _eventService.GetEventByIdAsync(id);
            return Ok(response);
        }
       
       
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var response = await _eventService.GetAllEventsAsync();
            return Ok(response);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDTO eventDto)
        {
            var response = await _eventService.UpdateEventAsync(id, eventDto);
            return Ok(response);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return Ok();
        }
    }
}