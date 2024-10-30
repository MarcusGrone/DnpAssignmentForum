using ApiContracts.Dto_User;

namespace ApiContracts.DTO_Comment;

public class CreateCommentDto
{
    public required string Body { get; set; }
    public required int AuthorId { get; set; }
}