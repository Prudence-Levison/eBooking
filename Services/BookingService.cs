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
        await using var transaction = await _context.Database.BeginTransactionAsync();
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
          
        var booking = new Booking
        {
            UserId = userId,
            EventId = bookingDto.EventId,
            NumberOfTickets = bookingDto.NumberOfTickets,
            TotalCost = eventItem.Cost * bookingDto.NumberOfTickets,
            Status = eBooking.Enums.BookingStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(2) 
        };
        var affectedRows = await _context.Events
       .Where(e => e.Id == bookingDto.EventId
        && e.AvailableTickets >= bookingDto.NumberOfTickets
        && e.AvailableSeats >= bookingDto.NumberOfTickets)
       .ExecuteUpdateAsync(setters => setters
        .SetProperty(e => e.AvailableTickets,
            e => e.AvailableTickets - bookingDto.NumberOfTickets)
        .SetProperty(e => e.AvailableSeats,
            e => e.AvailableSeats - bookingDto.NumberOfTickets));

        if (affectedRows == 0)
   {
        throw new Exception(
        "Event not found or insufficient tickets or seats available.");
 }
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new BookingDTO
        {
            Id = booking.Id,
            EventId = booking.EventId,
            EventTitle = eventItem.Title,
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
