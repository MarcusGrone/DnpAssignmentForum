using ApiContracts.Dto_User;

namespace ApiContracts.DTO_Post;

public class PostDto
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int AuthorId { get; set; }
}