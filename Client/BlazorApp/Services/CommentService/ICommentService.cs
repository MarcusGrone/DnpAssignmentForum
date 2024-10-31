using ApiContracts.DTO_Comment;

namespace BlazorApp.Services.CommentService;

public interface ICommentService
{
    Task<CommentDto> GetCommentByIdAsync(int postId);
    Task<CommentDto> CreateCommentAsync(int postId, CreateCommentDto dto);
    Task<CommentDto> UpdateCommentAsync(int commentId, UpdateCommentDto dto);
    Task<List<CommentDto>> GetCommentsForPostAsync(int postId);
    Task<bool> DeleteCommentAsync(int commentId);
    Task<IEnumerable<CommentDto>> GetCommentsAsync();
}