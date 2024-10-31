using RepositoryContracts;

namespace CLI.UI.Comment;

public class ManageCommentCLIView
{
    private readonly ICommentRepository _commentRepository;
    
    public ManageCommentCLIView(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
   /* public async Task ShowMenuAsync()
    {
        while(true)
        {
        Console.WriteLine("\nComment Management:");
        Console.WriteLine("1. Add Comment");
        Console.WriteLine("2. Return to main menu");

        var input = Console.ReadLine();

        switch (input)
        {
            case "1":
                var lifeCycleCommentCLIView =
                    new LifecycleCommentCLIView(_commentRepository);
                await lifeCycleCommentCLIView.CreateCommentAsync();
                break;
            case "2":
                Console.WriteLine("Returning to main menu.");
                return;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }*/
    
}
