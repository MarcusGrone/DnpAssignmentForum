using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcCommentRepository : ICommentRepository
{

    private readonly AppContext _ctx;

    public EfcCommentRepository(AppContext ctx)
    {
        _ctx = ctx;
    }


    public async Task<Comment> AddAsync(Comment comment)
    {
        var entityEntry = await _ctx.Comments.AddAsync(comment);
        await _ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Comment comment)
    {
        if (!await _ctx.Comments.AnyAsync(c => c.CommentId == comment.CommentId))
        {
            throw new KeyNotFoundException($"Comment with id {comment.CommentId} not found");
        }

        _ctx.Comments.Update(comment);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int commentId)
    {
        var existing = await _ctx.Comments.SingleOrDefaultAsync(c => c.CommentId == commentId);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Comment with id {commentId} not found");
        }

        _ctx.Comments.Remove(existing);
        await _ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int commentId)
    {
        return await _ctx.Comments
                   .Include(c => c.Post)
                   .Include(c => c.User)
                   .SingleOrDefaultAsync(c => c.CommentId == commentId)
               ?? throw new KeyNotFoundException($"Comment with id {commentId} not found");
    }

    public IQueryable<Comment> GetMany()
    {
        return _ctx.Comments.AsQueryable();
    }
}