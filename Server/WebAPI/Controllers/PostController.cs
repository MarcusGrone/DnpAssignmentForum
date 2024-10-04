using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

public class PostController : ControllerBase
{
    private readonly IPostRepository _postRepository;

    public PostController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        var createdPost = await _postRepository.AddAsync(post);
        return CreatedAtAction(nameof(GetSinglePost),
            new { id = createdPost.PostId }, createdPost);
    }


    [HttpPut("id")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
    {
        if (id != post.PostId)
        {
            return BadRequest("Unable to match ID");
        }

        var existingPost = await _postRepository.GetSingleAsync(id);
        if (existingPost == null)
        {
            return NotFound();
        }

        await _postRepository.UpdateAsync(post);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSinglePost(int id)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpGet]
    public IActionResult GetManyPosts([FromQuery] string author = null,
        [FromQuery] string title = null)
    {
        var posts = _postRepository.GetMany();
        return Ok(posts);
    }

    [HttpGet]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        await _postRepository.DeleteAsync(id);
        return NoContent();
    }
    
}