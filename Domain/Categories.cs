namespace eBooking.Domain
{
    public class Categories
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public string? Name { get; set; }

        public IEnumerable<Event> Events { get; set; } = []; // Navigation property to access categories /list of evnts. The user can only see the list of events but cannot modify it.

    }
}