using eBooking.Data;
using eBooking.Domain;
using eBooking.DTO;
using eBooking.Interfaces;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class BookingService:IBookingService
{
    private readonly ApplicationDbContext _context;

    private readonly IHttpContextAccessor _httpContextAccessor;
    public BookingService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task CancelBookingAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BookingDTO> CreateBookingAsync(CreateBookingDTO bookingDto)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?
        .User
        .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
        {
            throw new Exception("User is not authenticated.");
        }
        var userId = Guid.Parse(userIdClaim);

        var eventItem = await _context.Events.FindAsync(bookingDto.EventId);
        if (eventItem == null)
        {
            throw new Exception("Event not found.");
        }
        if (eventItem.AvailableTickets < bookingDto.NumberOfTickets)
        {
        throw new Exception("Not enough tickets available.");
        }  

        if (eventItem.AvailableSeats < bookingDto.NumberOfTickets)
        {
        throw new Exception("Not enough seats available.");
        }
        
        var booking = new Booking
        {
            UserId = userId,
            EventId = bookingDto.EventId,
            NumberOfTickets = bookingDto.NumberOfTickets,
            TotalCost = eventItem.Cost * bookingDto.NumberOfTickets,
            Status = eBooking.Enums.BookingStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(2) // Set expiration time to 2 hours from now
        };
        eventItem.AvailableTickets -= bookingDto.NumberOfTickets;
        eventItem.AvailableSeats -= bookingDto.NumberOfTickets;
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return new BookingDTO
        {
            Id = booking.Id,
            EventId = booking.EventId,
            NumberOfTickets = booking.NumberOfTickets,
            TotalCost = booking.TotalCost,
            Status = booking.Status,
            CreatedAt = booking.CreatedAt,
            ExpiresAt = booking.ExpiresAt
        };
    }

    public Task<IEnumerable<BookingDTO>> GetAllBookingsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BookingDTO> GetBookingByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
