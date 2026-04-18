using System.ComponentModel.DataAnnotations;
using eBooking.Enums;

namespace eBooking.Domain
{
    public class Event
    {
      public int Id { get; set; }
      public int AvailableSeats {get; set;}
      public int AvailableTickets {get; set;}
      public int TotalTickets {get; set;}
      public required string Title { get; set;}
      public string Location {get; set;} = null!;
      public string? Theme {get; set;}
      public EventStatus Status {get; set;} = EventStatus.Upcoming; 
      public string? Description {get; set;}
      public DateTime Date {get; set;}
      public decimal Cost {get; set;}

      public DateTime CreatedAt { get; set; } 
      public DateTime UpdatedAt { get; set; }
      public IEnumerable<Booking> Bookings {get; set;} = [];
    }
} 