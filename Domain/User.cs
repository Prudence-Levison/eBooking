using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eBooking.Domain
{
    public class User : IdentityUser<Guid>
    {
        public required string FirstName { get; set;}
        public required string LastName { get; set;}
        public DateTime CreatedAt { get; set;}
        
    }
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {
            Id = Guid.NewGuid();
            ConcurrencyStamp = Guid.NewGuid().ToString();

        }
    }
}