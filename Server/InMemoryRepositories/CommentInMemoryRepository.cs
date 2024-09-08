using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository

{
    private readonly List<Comment> comments;

    public CommentInMemoryRepository()
    {
        comments = new List<Comment>();
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
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    public Task<Comment> GetSingleAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Comment> GetMany()
    {
        throw new NotImplementedException();
    }
}