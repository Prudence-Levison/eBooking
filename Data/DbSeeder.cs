using eBooking.Domain;
using Microsoft.AspNetCore.Identity;

public class DbSeeder
{
private readonly UserManager<User> _userManager;
private readonly RoleManager<ApplicationRole> _roleManager;
private readonly IConfiguration _configuration;

public DbSeeder(UserManager<User> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
{
    _userManager = userManager;
    _roleManager = roleManager;
    _configuration = configuration;
}
public async  Task SeedAsync()
    {
       var adminRoleExists = await _roleManager.RoleExistsAsync("Admin");
       if (!adminRoleExists)
        {
            var adminRole = new ApplicationRole
            {
                Name = "Admin"
            };
           var result = await _roleManager.CreateAsync(adminRole);
           if (!result.Succeeded)
        {
            throw new Exception("Failed to create Admin role");
        }
        }
        var adminEmail = _configuration["AdminUser:Email"];
        var adminPassword = _configuration["AdminUser:Password"];

        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
       {
        throw new Exception("Admin email or password is not configured.");
       }
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            adminUser = new User
            {
                FirstName = "Admin",
                LastName = "User",
                Email = adminEmail,
                UserName = adminEmail,
            };
            var result = await _userManager.CreateAsync(adminUser, adminPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create Admin user");
            }
            await _userManager.AddToRoleAsync(adminUser, "Admin");
        }
         var roles = await _userManager.GetRolesAsync(adminUser);
         if (!roles.Contains("Admin"))
         {
            var result = await _userManager.AddToRoleAsync(adminUser, "Admin");
            if (!result.Succeeded)
            {
                throw new Exception("Failed to assign Admin role to Admin user");
            }
         }
        
    }
}