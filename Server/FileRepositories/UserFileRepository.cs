using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }


    public async Task<User> AddAsync(User user)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(userAsJson);
        
        int maxId = users.Count > 0 ? users.Max(u => u.UserId) : 0;
        user.UserId = maxId + 1;
        
        users.Add(user);
        
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);
        return user;
    }

    
    
    public async Task UpdateAsync(User user)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(userAsJson);

        var existingUser = users.FirstOrDefault(u => u.UserId == user.UserId);
        if (existingUser == null)
        {
            throw new InvalidCastException("User not found");
        }

        existingUser.UserName = user.UserName;
        existingUser.Password = user.Password;
        
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);

    }

    
    
    public async Task DeleteAsync(int userId)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(userAsJson);
        
        var userToRemove = users.FirstOrDefault(u => u.UserId == userId);
        if (userToRemove != null)
        {
            users.Remove(userToRemove);

            userAsJson = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(filePath, userAsJson);
        }
        else
        {
            throw new InvalidOperationException($"UserId:{userId} not found");
        }
    }

    
    
    public async Task<User> GetSingleAsync(int userId)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(userAsJson);

        return users.FirstOrDefault(u => u.UserId == userId);
    }

    
    
    public IQueryable<User> GetMany()
    {
        string userAsJson =  File.ReadAllTextAsync(filePath).Result;
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(userAsJson);

        return users.AsQueryable();

    }

    
    
    public void InitializeDummyData()
    {
        int nextUserId = 1;

        List<User> users = new List<User>
        {
            new User("john_doe", "password123") { UserId = nextUserId++ },
            new User("jane_smith", "mypassword") { UserId = nextUserId++ },
            new User("emily_clark", "emily2024") { UserId = nextUserId++ }
        };

        string userAsJson = JsonSerializer.Serialize(users);
        File.WriteAllText(filePath, userAsJson);
    }
}