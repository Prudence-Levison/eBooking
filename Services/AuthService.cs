using eBooking.Domain;
using eBooking.Interfaces;
using Microsoft.AspNetCore.Identity;
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;

    private readonly IRoleService _roleService;

    public AuthService(UserManager<User> userManager, IJwtService jwtService ,IRoleService roleService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _roleService = roleService;
    }
    
    public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null )
        {
             throw new Exception("Invalid email or password");
        }
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordValid)
        {
            throw new Exception("Invalid email or password");
        }
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtService.GenerateToken(user, roles);

        return new AuthResponseDTO
        {
            Token = token
        };
    }

    public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto)
    {
        if (registerDto.FirstName == "" || registerDto.LastName == "" || registerDto.Email == "" || registerDto.Password == "")
        {
            throw new Exception("All fields are required");
        }
        var user = await _userManager.FindByEmailAsync(registerDto.Email);
        if (user != null)
        {
            throw new Exception("User already exists");
        }
        var newUser = new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            UserName = registerDto.Email,
        };
        var result = await _userManager.CreateAsync(newUser, registerDto.Password);
        if (!result.Succeeded)
        {
         var errors = string.Join(", ", result.Errors.Select(e => e.Description));
         throw new Exception(errors);
} 
        await _roleService.CreateRole("Customer");
        await _roleService.AddToRole(newUser.Id, "Customer");

        var roles = await _userManager.GetRolesAsync(newUser);
        var token = _jwtService.GenerateToken(newUser, roles);
        return new AuthResponseDTO
        {
            Token = token
        };
}  }