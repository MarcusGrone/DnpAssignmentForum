using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> users;

    public UserInMemoryRepository()
    {
        users = new List<User>();
    }

    public Task<User> AddAsync(User user)
    {
        user.UserId = users.Any()
            ? users.Max(u => u.UserId) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetSingleAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<User> GetMany()
    {
        throw new NotImplementedException();
    }
}