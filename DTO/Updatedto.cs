namespace eBooking.DTO
{
    public class UpdateEventDTO
    {
        public int? AvailableSeats { get; set; }
        public int? AvailableTickets { get; set; }
        public int? TotalTickets { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public string? Theme { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Cost { get; set; }
    }
}