using ApiContracts.Dto_User;

namespace BlazorApp.Services.UserService;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
    Task<UserDto> GetSingleUserAsync(int userId);
    Task<IEnumerable<UserDto>> GetUserAsync();
    Task DeleteUserAsync(int userId);
}