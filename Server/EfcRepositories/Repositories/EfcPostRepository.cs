using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcPostRepository : IPostRepository
{
    private readonly AppContext _ctx;

    public EfcPostRepository(AppContext ctx)
    {
        this._ctx = ctx;
    }


    public async Task<Post> AddAsync(Post post)
    {
        EntityEntry<Post> entityEntry = await _ctx.Posts.AddAsync(post);
        await _ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Post post)
    {
        if (!await _ctx.Posts.AnyAsync(p => p.PostId == post.PostId))
        {
            throw new KeyNotFoundException($"Post with id {post.PostId} not found");
        }

        _ctx.Posts.Update(post);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int postId)
    {
        var post = await _ctx.Posts.SingleOrDefaultAsync(p => p.PostId == postId);
        if (post == null)
        {
            throw new KeyNotFoundException($"Post with id {postId} not found");
        }

        _ctx.Posts.Remove(post);
        await _ctx.SaveChangesAsync();
    }


    
    public async Task<Post> GetSingleAsync(int id)
    {
        var post = await _ctx.Posts
            .Include(p => p.User)   
            .Include(p => p.Comments) 
            .SingleOrDefaultAsync(p => p.PostId == id);

        if (post == null)
        {
            throw new KeyNotFoundException($"Post with id {id} not found");
        }

        return post;
    }


    public  IQueryable<Post> GetMany()
    {
        return _ctx.Posts.AsQueryable();
    }
}