using eBooking.DTO;

namespace eBooking.Interfaces
{
    public interface IEventService
    {
        Task<int> CreateEventAsync(CreateEventDTO eventDto);
        Task<Eventdto> GetEventByIdAsync(int id);
        Task<IEnumerable<Eventdto>> GetAllEventsAsync();
        Task<Eventdto> UpdateEventAsync(int id, UpdateEventDTO eventDto);
        Task DeleteEventAsync(int id);
    }    
}