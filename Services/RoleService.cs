using eBooking.Domain;
using eBooking.Interfaces;
using Microsoft.AspNetCore.Identity;

public class RoleService : IRoleService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    public RoleService(UserManager<User> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task CreateRole(string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (roleExists)
        {
            throw new Exception("Role already exists");
        }
        
        
         var role  = new ApplicationRole
         {
            Name = roleName
         };
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to create role");
        }
    }

    
    public async Task AddToRole(Guid userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            throw new Exception("Role does not exist");
        }

        var userAlreadyInRole = await _userManager.IsInRoleAsync(user, roleName);
        if (userAlreadyInRole)
        {
            throw new Exception("User is already in the role");
        }

        var result = await _userManager.AddToRoleAsync(user, roleName);

       
        if (!result.Succeeded)
        {
            throw new Exception("Failed to add user to role");
        }
    }

}