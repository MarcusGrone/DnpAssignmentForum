using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : iPostRepository
{
    private readonly string filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }


    public async Task<Post> AddAsync(Post post)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postAsJson);
        int maxId = posts.Count > 0 ? posts.Max(p => p.PostId) : 0;
        post.PostId = maxId + 1;
        posts.Add(post);
        postAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postAsJson);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int postId)
    {
        throw new NotImplementedException();
    }

    public async Task<Post> GetSingleAsync(int postId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Post> GetMany()
    {
        throw new NotImplementedException();
    }

    public void InitializeDummyData()
    {
        throw new NotImplementedException();
    }
}