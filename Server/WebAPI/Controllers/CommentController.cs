using ApiContracts.DTO_Comment;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

[ApiController]
[Route("/comments")]
public class CommentsController : ControllerBase
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;

    public CommentsController(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CreateCommentDto request)
    {
      
     /*   var author = await _userRepository.GetSingleAsync(request.AuthorId);
        if (author == null)
        {
            return BadRequest("Author not found.");
        }*/


        var post = await _postRepository.GetSingleAsync(request.PostId);
        if (post == null)
        {
            return BadRequest("Post not found.");
        }

        
        var comment = new Comment(request.Body, request.AuthorId, request.PostId);
        var createdComment = await _commentRepository.AddAsync(comment);

  
        var dto = new CommentDto
        {
            CommentId = createdComment.CommentId,
            Body = createdComment.Body,
            AuthorId = createdComment.UserId,
            PostId = createdComment.PostId
        };

        return CreatedAtAction(nameof(GetSingleComment), new { id = dto.CommentId }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto request)
    {
        var comment = await _commentRepository.GetSingleAsync(id);
        if (comment == null) return NotFound();

        comment.Body = request.Body;
        await _commentRepository.UpdateAsync(comment);

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetSingleComment(int id)
    {
        var comment = await _commentRepository.GetSingleAsync(id);
        if (comment == null) return NotFound();

        var dto = new CommentDto
        {
            CommentId = comment.CommentId,
            Body = comment.Body,
            AuthorId = comment.UserId,
            PostId = comment.PostId
        };

        return Ok(dto);
    }

    [HttpGet]
    public IActionResult GetManyCommentsFromPostId([FromQuery] int postId)
    {
        var comments = _commentRepository.GetMany()
            .Where(c => c.PostId == postId);
        
        var dtos = comments.Select(comment => new CommentDto
        {
            CommentId = comment.CommentId,
            Body = comment.Body,
            AuthorId = comment.UserId,
            PostId = comment.PostId
        }).ToList();

        return Ok(dtos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _commentRepository.GetSingleAsync(id);
        if (comment == null) return NotFound();

        await _commentRepository.DeleteAsync(id);
        return NoContent();
    }
}
