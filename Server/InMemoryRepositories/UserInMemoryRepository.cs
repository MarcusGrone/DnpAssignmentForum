using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> users;
    private bool isInitialized = false;

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
        User? existingPost = users.SingleOrDefault(u => u.UserId == user.UserId);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.UserId}' not found");
        }

        users.Remove(existingPost);
        users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int userId)
    {
        User? postToRemove = users.SingleOrDefault(u => u.UserId == userId);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{userId}' not found");
        }

        users.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int userId)
    {
        User? userToFind = users.SingleOrDefault(u => u.UserId == userId);
        if (userToFind is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{userId}' not found");
        }
        return Task.FromResult(userToFind);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(int userId)
    {
        throw new NotImplementedException();
    }


   
    
}