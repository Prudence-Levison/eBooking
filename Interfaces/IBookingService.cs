using eBooking.Domain;
using eBooking.DTO;
namespace eBooking.Interfaces
{
public interface IBookingService
{
    Task<BookingDTO> CreateBookingAsync(CreateBookingDTO bookingDto);
    Task<BookingDTO> GetBookingByIdAsync(int id);
    Task<IEnumerable<BookingDTO>> GetAllBookingsAsync();
    Task CancelBookingAsync(int id);
}
}