
using eBooking.Domain;
using eBooking.Interfaces;
using Microsoft.AspNetCore.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;

    public AuthService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto)
    {
        throw new NotImplementedException();
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
        };
        var result = await _userManager.CreateAsync(newUser, registerDto.Password);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to create user");
        }
        return new AuthResponseDTO
        {
            
        };
    }
}   