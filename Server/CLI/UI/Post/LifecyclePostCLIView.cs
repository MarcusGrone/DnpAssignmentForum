using Entities;
using RepositoryContracts;

public class LifecyclePostCLIView
{
    private readonly IPostRepository _postRepository;

    public LifecyclePostCLIView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task ExecuteAsync()
    {
        while (true)
        {
            Console.WriteLine("1. Create post");
            Console.WriteLine("2. Update post");
            Console.WriteLine("3. Delete post");
            Console.WriteLine("4. Return to previous menu");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await CreatePostAsync();
                    break;
                case "2":
                    await UpdatePostAsync();
                    break;
                case "3":
                    await DeletePostAsync();
                    break;
                case "4":
                    Console.WriteLine("Returning to previous menu.");
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private async Task CreatePostAsync()
    {
        Console.WriteLine("Enter post title:");
        var title = Console.ReadLine();

        Console.WriteLine("Enter post body:");
        var body = Console.ReadLine();

        var newPost = new Post(title, body);
        await _postRepository.AddAsync(newPost);

        Console.WriteLine("Post created successfully.");
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }


    private async Task UpdatePostAsync()
    {
        Console.WriteLine("Enter post ID to update:");
        if (int.TryParse(Console.ReadLine(), out var postId))
        {
            try
            {
                var post = await _postRepository.GetSingleAsync(postId);
                if (post != null)
                {
                    Console.WriteLine($"Found Post: {post.Title}");
                    Console.WriteLine("Enter new title:");
                    post.Title = Console.ReadLine();

                    Console.WriteLine("Enter new body:");
                    post.Body = Console.ReadLine();

                    await _postRepository.UpdateAsync(post);

                    Console.WriteLine("Post updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Post with ID '{postId}' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to comply: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid post ID format.");
        }

        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }


    private async Task DeletePostAsync()
    {
        Console.WriteLine("Enter post ID to delete:");
        if (int.TryParse(Console.ReadLine(), out var postId))
        {
            try
            {
                var post = await _postRepository.GetSingleAsync(postId);
                if (post != null)
                {
                    await _postRepository.DeleteAsync(postId);
                    Console.WriteLine("Post deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Post with ID '{postId}' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to comply: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid post ID format.");
        }

        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }

}
