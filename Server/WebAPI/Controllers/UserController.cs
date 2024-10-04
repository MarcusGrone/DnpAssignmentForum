using ApiContracts.Dto_User;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

   
    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    {
    
        var user = new User(request.UserName, request.Password);
        var createdUser = await _userRepository.AddAsync(user);

        var dto = new UserDto
        {
            Id = createdUser.Id,
            UserName = createdUser.UserName
        };

        return CreatedAtAction(nameof(GetSingleUser), new { id = dto.Id }, dto);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto request)
    {
        var user = await _userRepository.GetSingleAsync(id);
        if (user == null) return NotFound();

        user.UserName = request.UserName;
        user.Password = request.Password;

        await _userRepository.UpdateAsync(user);
        return NoContent();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetSingleUser(int id)
    {
        var user = await _userRepository.GetSingleAsync(id);
        if (user == null) return NotFound();

        var dto = new UserDto
        {
            Id = user.Id,
            UserName = user.UserName
        };

        return Ok(dto);
    }


    [HttpGet]
    public IActionResult GetManyUsers([FromQuery] string? userNameFilter)
    {
        var users = _userRepository.GetMany();

 
        if (!string.IsNullOrEmpty(userNameFilter))
        {
            users = users.Where(u => u.UserName.Contains(userNameFilter, StringComparison.OrdinalIgnoreCase));
        }

        var dtos = users.Select(user => new UserDto
        {
            Id = user.Id,
            UserName = user.UserName
        });

        return Ok(dtos);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userRepository.GetSingleAsync(id);
        if (user == null) return NotFound();

        await _userRepository.DeleteAsync(id);
        return NoContent();
    }
}