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
    public async Task CancelBookingAsync(int id)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?
        .User
        .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userIdClaim == null)
        {
            throw new Exception("User is not authenticated.");
        }
        var userId = Guid.Parse(userIdClaim);
        var booking = await _context.Bookings
        .Where(b => b.Id == id && b.UserId == userId)
        .FirstOrDefaultAsync();
        if (booking == null)
        {
            throw new Exception("Booking not found.");
        }
        if (booking.Status != eBooking.Enums.BookingStatus.Pending)
        {
            throw new Exception("Only pending bookings can be cancelled.");
        }
        var eventItem = await _context.Events.FindAsync(booking.EventId);
        if (eventItem == null)
        {
            throw new Exception("Event not found.");
        }


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

    public async Task<IEnumerable<BookingDTO>> GetAllBookingsAsync()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?
        .User
        .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
        {
            throw new Exception("User is not authenticated.");
        }
        var userId = Guid.Parse(userIdClaim);
        var bookings = await _context.Bookings
            .AsNoTracking()
            .Include(b => b.Event)
            .Where(b => b.UserId == userId)
            .ToListAsync();
        return bookings.Select(booking => new BookingDTO
        {
            Id = booking.Id,
            EventId = booking.EventId,
            EventTitle = booking.Event.Title,
            NumberOfTickets = booking.NumberOfTickets,
            TotalCost = booking.TotalCost,
            Status = booking.Status,
            CreatedAt = booking.CreatedAt,
            ExpiresAt = booking.ExpiresAt
        });
    }

    public async Task<BookingDTO?> GetBookingByIdAsync(int id)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?
        .User
        .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
        {
            throw new Exception("User is not authenticated.");
        }
        var userId = Guid.Parse(userIdClaim);
        var booking  =await _context.Bookings
        .AsNoTracking()
        .Include(b => b.Event)
        .Where(b => b.Id == id && b.UserId == userId)
        .FirstOrDefaultAsync();
        if (booking == null)
        {
            return null;
        }
        return new BookingDTO
        {
            Id = booking.Id,
            EventId = booking.EventId,
            EventTitle = booking.Event.Title,
            NumberOfTickets = booking.NumberOfTickets,
            TotalCost = booking.TotalCost,
            Status = booking.Status,
            CreatedAt = booking.CreatedAt,
            ExpiresAt = booking.ExpiresAt
        };
       
    }
}
