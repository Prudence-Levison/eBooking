using eBooking.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        var response = await _authService.RegisterAsync(registerDto);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var response = await _authService.LoginAsync(loginDto);
        return Ok(response);
    }
}