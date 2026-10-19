using eBooking.DTO;
using eBooking.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace eBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody]CreateBookingDTO bookingDto)
        {
            var booking = await _bookingService.CreateBookingAsync(bookingDto);
            return Ok(booking);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
        [HttpGet]
         public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            if (bookings == null)
            {
                return NotFound();
            }
            return Ok(bookings);  
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            await _bookingService.CancelBookingAsync(id);
            return Ok();
        }
    }
    
}