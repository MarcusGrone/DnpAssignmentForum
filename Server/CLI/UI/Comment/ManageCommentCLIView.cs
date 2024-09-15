using RepositoryContracts;

namespace CLI.UI.Comment;

public class ManageCommentCLIView
{
    private readonly ICommentRepository UIcommentRepository;
    
    public ManageCommentCLIView(ICommentRepository commentRepository)
    {
        UIcommentRepository = commentRepository;
    }
    
    public async Task ShowMenuAsync()
    {
        throw new NotImplementedException();
    }
}