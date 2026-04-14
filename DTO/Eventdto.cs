namespace eBooking.DTO
{
    public class Eventdto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Location { get; set; }
        public string? Theme { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
        public int AvailableSeats { get; set; }
        public int AvailableTickets { get; set; }
        public int TotalTickets { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}