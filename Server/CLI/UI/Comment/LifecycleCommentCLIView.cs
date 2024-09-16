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
        Console.WriteLine("Enter body:");
        var body = Console.ReadLine();

        var newComment = new Entities.Comment(body);
        
        await _commentRepository.AddAsync(newComment);
        
        Console.WriteLine($"Comment {newComment}, was created successfully");
        
    }
}