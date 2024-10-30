using ApiContracts.Dto_User;

namespace BlazorApp.Services.UserService;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto request);
    Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto request);
    Task<UserDto> GetSingleUserAsync(int userId);
    Task<List<UserDto>>GetUserAsync();
    Task DeleteUserAsync(int userId);
}