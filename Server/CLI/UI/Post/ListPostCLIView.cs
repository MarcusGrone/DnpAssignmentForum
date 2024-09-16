using RepositoryContracts;

namespace CLI.UI.Post;

public class ListPostCLIView
{
    private readonly iPostRepository _postRepository;

    public ListPostCLIView(iPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task ShowAllPostsAsync()
    {
        var posts = _postRepository.GetMany().ToList();

        if (!posts.Any())
        {
            Console.WriteLine("No posts available.");
        }
        else
        {
            foreach (var post in posts)
            {
                Console.WriteLine(
                    $"Post ID: {post.PostId}, Title: {post.Title}");
            }
        }

        await Task.CompletedTask;
    }
}
