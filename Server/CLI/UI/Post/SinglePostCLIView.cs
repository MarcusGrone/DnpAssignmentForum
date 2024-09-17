using RepositoryContracts;

namespace CLI.UI.Post;

public class SinglePostCLIView
{
    private readonly iPostRepository _postRepository;

    public SinglePostCLIView(iPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task ShowSinglePostAsync()
    {
        Console.WriteLine("Enter the Post ID:");
        if (int.TryParse(Console.ReadLine(), out var postId))
        {
            try
            {
                var post = await _postRepository.GetSingleAsync(postId);
                if (post != null)
                {
                    Console.WriteLine($"Post ID: {post.PostId}");
                    Console.WriteLine($"Title: {post.Title}");
                    Console.WriteLine($"Body: {post.Body}");
                }
                else
                {
                    Console.WriteLine("Post not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to comply5: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid Post ID format.");
        }

        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }

}