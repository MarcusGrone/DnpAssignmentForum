using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }


    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentAsJson);

        int maxId = comments.Count > 0 ? comments.Max(c => c.CommentId) : 0;
        comment.CommentId = maxId + 1;

        comments.Add(comment);

        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentAsJson);

        var existingComment =
            comments.FirstOrDefault(c =>
                c.CommentId == comment.CommentId);
        if (existingComment == null)
        {
            throw new InvalidOperationException("Comment not found");
        }

        existingComment.Body = comment.Body;

        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
    }


    public async Task DeleteAsync(int commentId)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentAsJson);

        var commentToRemove =
            comments.FirstOrDefault(c =>
                c.CommentId == commentId);
        if (commentToRemove != null)
        {
            comments.Remove(commentToRemove);

            commentAsJson = JsonSerializer.Serialize(comments);
            await File.WriteAllTextAsync(filePath, commentAsJson);
        }
        else
        {
            throw new InvalidOperationException(
                $"CommentId:{commentId} not found");
        }
    }


    public async Task<Comment> GetSingleAsync(int commentId)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentAsJson);

        return comments.FirstOrDefault(c => c.CommentId == commentId);
    }


    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);

        return comments.AsQueryable();
    }


 /*   public void InitializeDummyData()
    {
        int nextCommentId = 1;
        
        List<Comment> comments = new List<Comment>
        {
            new Comment("First comment") { CommentId = nextCommentId++ },
            new Comment("Second comment") { CommentId = nextCommentId++ },
            new Comment("Another comment") { CommentId = nextCommentId++ }
        };

       
        string commentAsJson = JsonSerializer.Serialize(comments);
        File.WriteAllText(filePath, commentAsJson);
    }*/
}