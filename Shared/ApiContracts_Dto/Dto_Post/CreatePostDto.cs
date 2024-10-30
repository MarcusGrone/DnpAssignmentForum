using ApiContracts.Dto_User;

namespace ApiContracts.DTO_Post;

public class CreatePostDto
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required int AuthorId { get; set; }
}