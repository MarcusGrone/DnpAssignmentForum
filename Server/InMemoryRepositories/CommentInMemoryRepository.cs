using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository

{
    private readonly List<Comment> comments;

    public CommentInMemoryRepository()
    {
        comments = new List<Comment>();
        InitializeDummyData();
    }

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.CommentId = comments.Any()
            ? comments.Max(c => c.CommentId) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }
    

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.CommentId == comment.CommentId);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.CommentId}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int commentId)
    {
        Comment? postToRemove = comments.SingleOrDefault(c => c.CommentId == commentId);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{commentId}' not found");
        }

        comments.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int commentId)
    {
        Comment? commentToFind = comments.SingleOrDefault(c => c.CommentId == commentId);
        if (commentToFind is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{commentId}' not found");
        }
        return Task.FromResult(commentToFind);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }
    
    public void InitializeDummyData()
    {
        comments.AddRange(new List<Comment>
        {
            new Comment { CommentId = 1, Body = "First comment" },
            new Comment { CommentId = 2, Body = "Second comment" },
            new Comment { CommentId = 3, Body = "Another comment" },
        });
    }
}