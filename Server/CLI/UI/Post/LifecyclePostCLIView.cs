using Entities;
using RepositoryContracts;

public class LifecyclePostCLIView
{
    private readonly iPostRepository _postRepository;

    public LifecyclePostCLIView(iPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task ExecuteAsync()
    {
        Console.WriteLine("1. Create post");
        Console.WriteLine("2. Update post");
        Console.WriteLine("3. Delete post");

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
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }

    private async Task CreatePostAsync()
    {
        Console.WriteLine("Enter post title:");
        var title = Console.ReadLine();

        Console.WriteLine("Enter post content:");
        var content = Console.ReadLine();

        var newPost = new Post(title, content);
        await _postRepository.AddAsync(newPost);

        Console.WriteLine("Post created successfully.");
    }

    private async Task UpdatePostAsync()
    {
        Console.WriteLine("Enter post ID to update:");
        if (int.TryParse(Console.ReadLine(), out var postId))
        {
            var post = await _postRepository.GetSingleAsync(postId);
            if (post != null)
            {
                Console.WriteLine("Enter new title:");
                post.Title = Console.ReadLine();

                Console.WriteLine("Enter new body:");
                post.Body = Console.ReadLine();

                await _postRepository.UpdateAsync(post);

                Console.WriteLine("Post updated successfully.");
            }
        }
        else
        {
            Console.WriteLine("Invalid post ID.");
        }
    }

    private async Task DeletePostAsync()
    {
        Console.WriteLine("Enter post ID to delete:");
        if (int.TryParse(Console.ReadLine(), out var postId))
        {
            await _postRepository.DeleteAsync(postId);
            Console.WriteLine("Post deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid post ID.");
        }
    }
}