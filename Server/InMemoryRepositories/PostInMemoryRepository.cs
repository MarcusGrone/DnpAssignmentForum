using System.Formats.Tar;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : iPostRepository
{
    private readonly List<Post> posts;

    public PostInMemoryRepository()
    {
        posts = new List<Post>();
    }

    public Task<Post> AddAsync(Post post)
    {
        post.PostId = posts.Any()
                ? posts.Max(p => p.PostId) + 1
                : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int postId)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetSingleAsync(int postId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Post> GetMany()
    {
        throw new NotImplementedException();
    }
}