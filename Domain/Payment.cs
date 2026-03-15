using eBooking.Enums;

namespace eBooking.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; }

        public Booking? Booking { get; set; } // Navigation property to access the associated booking details. 

        public int UserId { get; set; }

        public int EventId { get; set; }// I wondered if there should be a relationship between payment and Events directly seeing as we have a relationship between payment and booking and there is an relationship between events and booking.

        public int walletId { get; set; }

        public Wallet? Wallet { get; set; } // Navigation property to access the associated wallet details.
        
        public decimal Amount { get; set; }
        public required  string PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; } // Pending, Successful, Failed, Refunded
        public string? Description { get; set; }
        public string? PaymentHistory { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}