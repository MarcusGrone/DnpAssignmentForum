using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
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
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postAsJson);

        var exsistingPost = posts.FirstOrDefault(p => p.PostId == post.PostId);
        if (exsistingPost == null)
        {
            throw new InvalidCastException("Post not found");
        }

        exsistingPost.Title = post.Title;
        exsistingPost.Body = post.Body;

        postAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postAsJson);
     
    }

    
    
    public async Task DeleteAsync(int postId)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postAsJson);

        var postToRemove = posts.FirstOrDefault(p => p.PostId == postId);
        if (postToRemove != null)
        {
            posts.Remove(postToRemove);

            postAsJson = JsonSerializer.Serialize(posts);
            await File.WriteAllTextAsync(filePath, postAsJson);
        }
        else
        {
            throw new InvalidOperationException($"postId:{postId} not found");
        }
    }

    
    
    public async Task<Post> GetSingleAsync(int postId)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postAsJson);

        return posts.FirstOrDefault(p => p.PostId == postId);
    }

    
    
    public IQueryable<Post> GetMany()
    {
        string postAsJson =  File.ReadAllTextAsync(filePath).Result;
        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postAsJson);

        return posts.AsQueryable();

    }

    
    
   /* public void InitializeDummyData()
    {
        int nextPostId = 1;

        List<Post> posts = new List<Post>
        {
            new Post("First Post", "This is the first post.") { PostId = nextPostId++ },
            new Post("Second Post", "This is the second post.") { PostId = nextPostId++ }
        };

        string postAsJson = JsonSerializer.Serialize(posts);
        File.WriteAllText(filePath, postAsJson);
    } */
}