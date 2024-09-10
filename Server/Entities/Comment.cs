namespace Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Body { get; set; }

    public Comment(int commentId, string body)
    {
        CommentId = commentId;
        Body = body;
    }

  
}