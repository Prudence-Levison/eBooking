namespace eBooking.Wrappers
{
    public class PaginatedResult<T>
    {
        public int Pages { get; set; }
        public int Limit { get; set; }
        public int TotalEvents { get; set; }

        public int TotalPages { get; set; }
        public List<T> Data { get; set; }

    }
}