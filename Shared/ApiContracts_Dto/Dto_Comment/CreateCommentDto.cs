using ApiContracts.Dto_User;

namespace ApiContracts.DTO_Comment;

public class CreateCommentDto
{
    public string Body { get; set; }
    public int AuthorId { get; set; }
    public int PostId { get; set; }
}