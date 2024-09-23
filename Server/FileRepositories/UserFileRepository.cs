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
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetSingleAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<User> GetMany()
    {
        throw new NotImplementedException();
    }

    public void InitializeDummyData()
    {
        throw new NotImplementedException();
    }
}