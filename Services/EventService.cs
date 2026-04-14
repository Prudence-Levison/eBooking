using System.Reflection.Metadata.Ecma335;
using eBooking.Data;
using eBooking.Domain;
using eBooking.DTO;
using eBooking.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace eBooking.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEventAsync(CreateEventDTO eventDto)
        {
            if (string.IsNullOrEmpty(eventDto.Title))
            {
                throw new ArgumentException("Event title is required.");
            }
            if (eventDto.Date < DateTime.Now)
            {
                throw new ArgumentException("Event date must be in the future.");

            }
            
         // what happens if available ticket and total tickets are not same value, should we allow that?
        //  How to validate that the available tickets should not be more than total tickets, and also should not be less than zero
        //  How can we validate that the total tickets should not be less than zero
        // What are other possible validations we can add for creating an event?
        
        if (eventDto.AvailableTickets > eventDto.TotalTickets){
            throw new ArgumentException("Availabe Ticket must not be greater than Total Tickets");
        };
            if(eventDto.AvailableTickets  < 0 || eventDto.TotalTickets  < 0)
            {
                throw new ArgumentException("Available tickets and total tickets must be greater than zero.");
            }
            if(eventDto.AvailableTickets > eventDto.TotalTickets)
            {
                throw new ArgumentException("Available tickets cannot be more than total tickets.");
            }
            var newEvent = new Event
            {
                Title = eventDto.Title,
                Location = eventDto.Location,
                Theme = eventDto.Theme,
                Description = eventDto.Description,
                Date = eventDto.Date,
                Cost = eventDto.Cost,
                AvailableSeats = eventDto.AvailableSeats,
                AvailableTickets = eventDto.AvailableTickets,
                TotalTickets = eventDto.TotalTickets
            };

           await  _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return newEvent.Id;
        }

        public async Task<Eventdto> GetEventByIdAsync(int id)
        {
            var eventData = await _context.Events.FindAsync(id);

            if (eventData == null)
            {
            throw new KeyNotFoundException($"Event with ID {id} not found.");
                
            }
            return new Eventdto
            {
                Id = eventData.Id,
                Title = eventData.Title,
                Location = eventData.Location,
                Theme = eventData.Theme,
                Description = eventData.Description,
                Date = eventData.Date,
                Cost = eventData.Cost,
                AvailableSeats = eventData.AvailableSeats,
                AvailableTickets = eventData.AvailableTickets,
                TotalTickets = eventData.TotalTickets,
                CreatedAt = eventData.CreatedAt,
                UpdatedAt = eventData.UpdatedAt
            };
        }

        public async Task<IEnumerable<Eventdto>> GetAllEventsAsync()
        {
            return await _context.Events
            .Select(e => new Eventdto 
            {
                Id = e.Id,
                Title = e.Title,
                Location = e.Location,
                Theme = e.Theme,
                Description = e.Description,
                Date = e.Date,
                Cost = e.Cost,
                AvailableSeats = e.AvailableSeats,
                AvailableTickets = e.AvailableTickets,
                TotalTickets = e.TotalTickets,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            }).ToListAsync();
        }  
        public  async Task<Eventdto> UpdateEventAsync(int id, UpdateEventDTO eventDto)  
        {
            var existingEvent = await _context.Events.FindAsync(id);

            if (existingEvent == null)
            {
                return null;
            }
            
            existingEvent.Title = eventDto.Title;
            existingEvent.Location = eventDto.Location;
            existingEvent.Theme = eventDto.Theme;  
            existingEvent.Description = eventDto.Description;
            existingEvent.Date = eventDto.Date;
            existingEvent.Cost = eventDto.Cost;
            existingEvent.AvailableSeats = eventDto.AvailableSeats;
            existingEvent.AvailableTickets = eventDto.AvailableTickets;
            existingEvent.TotalTickets = eventDto.TotalTickets;
            existingEvent.UpdatedAt = DateTime.Now;
            
            await _context.SaveChangesAsync();

            return new Eventdto
            {
                Id = existingEvent.Id,
                Title = existingEvent.Title,
                Location = existingEvent.Location,
                Theme = existingEvent.Theme,
                Description = existingEvent.Description,
                Date = existingEvent.Date,
                Cost = existingEvent.Cost,
                AvailableSeats = existingEvent.AvailableSeats,
                AvailableTickets = existingEvent.AvailableTickets,
                TotalTickets = existingEvent.TotalTickets,
                CreatedAt = existingEvent.CreatedAt,
                UpdatedAt = existingEvent.UpdatedAt
            };

            
        }

        public async Task DeleteEventAsync(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);

            if (eventToDelete == null)
            {
                return;
            }

            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
        }
    }
}