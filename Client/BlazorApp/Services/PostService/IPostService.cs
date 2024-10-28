using ApiContracts.DTO_Comment;
using ApiContracts.DTO_Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services.PostService
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetPostsAsync();
        Task<PostDto> GetPostByIdAsync(int postId);
        Task<PostDto> CreatePostAsync(CreatePostDto dto);
        Task<PostDto> UpdatePostAsync(int postId, UpdatePostDto dto);
        Task<bool> DeletePostAsync(int postId);
        Task<IEnumerable<CommentDto>> GetCommentsForPostAsync(int postId);
    }
}