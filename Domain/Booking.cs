using eBooking.Enums; 
namespace eBooking.Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Event? Event { get; set;} 
        public int EventId { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalCost { get; set; }
        public BookingStatus Status { get; set; } // Confirmed, Cancelled, Pending
        public DateTime CreatedAt { get; set; }
        
    }
}