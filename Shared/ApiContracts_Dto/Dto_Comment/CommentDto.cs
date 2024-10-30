using ApiContracts.DTO_Post;
using ApiContracts.Dto_User;

namespace ApiContracts.DTO_Comment;

public class CommentDto
{
    public int CommentId { get; set; }
    public string Body { get; set; }
    public UserDto AuthorId { get; set; }
    public PostDto PostId { get; set; }
}