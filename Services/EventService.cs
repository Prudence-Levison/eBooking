using System.Reflection.Metadata.Ecma335;
using eBooking.Data;
using eBooking.Domain;
using eBooking.DTO;
using eBooking.Interfaces;
using eBooking.Wrappers;
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

        public async Task<int> CreateAsync(CreateEventDTO eventDto)
        {
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
        public async Task<Eventdto> GetByIdAsync(int id)
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
        public async Task<PaginatedResult<Eventdto>> GetAllAsync(int page, int limit)
        {
            var query = _context.Events
            .AsNoTracking()
            .AsQueryable();

            var totalEvents = await query.CountAsync();

            query = query.OrderBy(e => e.Id);

            var events = await query
            .Skip((page - 1) * limit)
            .Take(limit)
            
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

            return new PaginatedResult<Eventdto>
            {
                Pages = page,
                Limit = limit,
                TotalEvents = totalEvents,
                TotalPages = (int)Math.Ceiling(totalEvents / (double)limit),
                Data = events
            };
        }  
        public  async Task<Eventdto> UpdateAsync(int id, UpdateEventDTO eventDto)  
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
            {
                throw new KeyNotFoundException($"Event with ID {id} not found.");
            }

            if (eventDto.Title != null)
            existingEvent.Title = eventDto.Title;

            if (eventDto.Location != null)
            existingEvent.Location = eventDto.Location;

            if (eventDto.Theme != null)
            existingEvent.Theme = eventDto.Theme;  

            if (eventDto.Description != null)
            existingEvent.Description = eventDto.Description;

            if (eventDto.Date.HasValue)
            existingEvent.Date = eventDto.Date.Value;

            if (eventDto.Cost.HasValue)
            existingEvent.Cost = eventDto.Cost.Value;

            if (eventDto.AvailableSeats.HasValue)
            existingEvent.AvailableSeats = eventDto.AvailableSeats.Value;

            if (eventDto.AvailableTickets.HasValue)
            existingEvent.AvailableTickets = eventDto.AvailableTickets.Value;

            if (eventDto.TotalTickets.HasValue)
            existingEvent.TotalTickets = eventDto.TotalTickets.Value;

            existingEvent.UpdatedAt = DateTime.UtcNow;
            
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

        public async Task DeleteAsync(int id)
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