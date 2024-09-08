using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(Comment user);
    Task UpdateAsync(Comment user);
    Task DeleteAsync(int userId);
    Task<User> GetSingleAsync(int userId);
    IQueryable<User> GetMany();
}