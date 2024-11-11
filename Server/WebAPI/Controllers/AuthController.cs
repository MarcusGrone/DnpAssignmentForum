using ApiContracts.Dto_LoginRequest;
using ApiContracts.Dto_User;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        Console.WriteLine($"Received login request for username: {request.UserName}");
        var user = await _userRepository.GetByUsernameAsync(request.UserName);
    
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return Unauthorized("Invalid username or password");
        }

        Console.WriteLine($"User found: {user.UserName}, Password: {user.Password}");
        if (user.Password != request.Password)
        {
            Console.WriteLine("Password mismatch.");
            return Unauthorized("Invalid username or password");
        }

        var userDto = new UserDto
        {
            Id = user.UserId,
            UserName = user.UserName
        };

        return Ok(userDto);
    }

}