using eBooking.Enums;

namespace eBooking.Domain
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; } // Navigation property to access the associated event details.

        public int WalletId { get; set; }

        public Wallet Wallet { get; set; } // Navigation property to access the associated wallet details.

        public int TotalTransactions { get; set; }

        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string? TransactionType { get; set; }
        public required string PaymentMethod { get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; } 
        public required string ReferenceNumber { get; set; } // Unique reference number for the transaction, useful for tracking and reconciliation.
    }
}