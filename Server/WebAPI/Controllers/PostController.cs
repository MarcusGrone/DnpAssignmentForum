using ApiContracts.DTO_Post;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("/posts")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;


    public PostsController(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository  ?? throw new ArgumentNullException(nameof(postRepository));
        _userRepository = userRepository  ?? throw new ArgumentNullException(nameof(userRepository));
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostDto request)
    {
        var post = new Post(request.Title, request.Body, request.AuthorId);
        var createdPost = await _postRepository.AddAsync(post);

        var dto = new PostDto
        {
            PostId = createdPost.PostId,
            Title = createdPost.Title,
            Body = createdPost.Body,
            AuthorId = createdPost.UserId
        };

        return CreatedAtAction(nameof(GetSinglePost), new { id = dto.PostId }, dto);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDto request)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post == null) return NotFound();

        post.Title = request.Title;
        post.Body = request.Body;

        await _postRepository.UpdateAsync(post);
        return NoContent();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetSinglePost(int id)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post == null) return NotFound();

        var dto = new PostDto
        {
            PostId = post.PostId,
            Title = post.Title,
            Body = post.Body
        };

        return Ok(dto);
    }


    [HttpGet]
    public IActionResult GetManyPosts([FromQuery] string? titleFilter, [FromQuery] string? authorNameFilter)
    {
        var posts = _postRepository.GetMany();

        if (!string.IsNullOrEmpty(titleFilter))
        {
            posts = posts.Where(p => p.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase));
        }

        var dtos = posts.Select(post => new PostDto
        {
            PostId = post.PostId,
            Title = post.Title,
            Body = post.Body
        });

        return Ok(dtos);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post == null) return NotFound();

        await _postRepository.DeleteAsync(id);
        return NoContent();
    }
}
