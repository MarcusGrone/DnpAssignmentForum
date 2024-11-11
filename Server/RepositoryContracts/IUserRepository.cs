using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int userId);
    Task<User> GetSingleAsync(int userId);
    IQueryable<User> GetMany();
    Task<User?> GetByUsernameAsync(string username); // Optional for finding by username
    Task<bool> ExistsAsync(int userId);
}