namespace eBooking.Interfaces
{
    public interface IRoleService
    {
        Task AddToRole(Guid userId, string roleName);
        Task CreateRole(string roleName);
    }
}