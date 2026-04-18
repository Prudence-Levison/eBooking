using eBooking.DTO;

namespace eBooking.Interfaces
{
    public interface IEventService
    {
        Task<int> CreateAsync(CreateEventDTO eventDto);
        Task<Eventdto> GetByIdAsync(int id);
        Task<IEnumerable<Eventdto>> GetAllAsync();
        Task<Eventdto> UpdateAsync(int id, UpdateEventDTO eventDto);
        Task DeleteAsync(int id);
    }    
}