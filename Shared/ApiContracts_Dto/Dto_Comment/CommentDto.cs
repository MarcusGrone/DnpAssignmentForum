using ApiContracts.DTO_Post;
using ApiContracts.Dto_User;

namespace ApiContracts.DTO_Comment;

public class CommentDto
{
    public int CommentId { get; set; }
    public string Body { get; set; }
    public int AuthorId { get; set; }
    public int PostId { get; set; }
}