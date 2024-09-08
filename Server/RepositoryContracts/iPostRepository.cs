using Entities;

namespace RepositoryContracts;

public interface iPostRepository
{
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int postId);
    Task<Post> GetSingleAsync(int postId);
    IQueryable<Post> GetMany();
}