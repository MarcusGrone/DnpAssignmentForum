namespace Entities;

public class Comment
{
    public int CommentId { get;  set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Body { get; set; }
    

    public Comment(string body, int userId, int postId)
    {
        Body = body;
        UserId = userId;
        PostId = postId;
    }

    public Comment()
    {
    
    }
}