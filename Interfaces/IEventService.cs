using eBooking.DTO;
using eBooking.Wrappers;

namespace eBooking.Interfaces
{
    public interface IEventService
    {
        Task<int> CreateAsync(CreateEventDTO eventDto);
        Task<Eventdto> GetByIdAsync(int id);
        Task<PaginatedResult<Eventdto>> GetAllAsync(int page, int limit);
        Task<Eventdto> UpdateAsync(int id, UpdateEventDTO eventDto);
        Task DeleteAsync(int id);
    }    
}