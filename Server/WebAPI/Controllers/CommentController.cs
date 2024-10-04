using ApiContracts.DTO_Comment;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;

    public CommentsController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

   
    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CreateCommentDto request)
    {
        var comment = new Comment(request.Body);
        var createdComment = await _commentRepository.AddAsync(comment);

        var dto = new CommentDto
        {
            CommentId = createdComment.CommentId,
            Body = createdComment.Body
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
            Body = comment.Body
        };

        return Ok(dto);
    }


    [HttpGet]
    public IActionResult GetManyComments([FromQuery] int? commentId)
    {
        var comments = _commentRepository.GetMany();

        if (commentId.HasValue)
        {
            comments = comments.Where(c => c.CommentId == commentId.Value);
        }

        var dtos = comments.Select(comment => new CommentDto
        {
            CommentId = comment.CommentId,
            Body = comment.Body
        });

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
