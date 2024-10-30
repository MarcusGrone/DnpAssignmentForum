namespace Entities;

public class Post
{
    public int PostId { get;  set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public Post(int postId, int userId, string title, string body)
    {
        PostId = postId;
        UserId = userId;
        Title = title;
        Body = body;
    }

    public Post(string requestTitle, string requestBody)
    {
        throw new NotImplementedException();
    }
}