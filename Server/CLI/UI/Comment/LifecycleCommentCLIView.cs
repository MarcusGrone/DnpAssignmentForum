using RepositoryContracts;

namespace CLI.UI.Comment;

public class LifecycleCommentCLIView
{
    private readonly ICommentRepository _commentRepository;

    public LifecycleCommentCLIView(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    

    public async Task CreateCommentAsync()
    {
        Console.WriteLine("Enter comment:");
        var body = Console.ReadLine();

        var newComment = new Entities.Comment(body);
        
        await _commentRepository.AddAsync(newComment);
        
        Console.WriteLine($"Comment {newComment.Body}, was created successfully");
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey(); 
    }
}