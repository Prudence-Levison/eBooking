using System;
using eBooking.Enums;

namespace eBooking.DTO
{
public class BookingDTO
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string EventTitle { get; set; }
    public int NumberOfTickets { get; set; }
    public decimal TotalCost { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}
}