using RepositoryContracts;

namespace CLI.UI.Post;

public class ManagePostCLIView
{
    private readonly iPostRepository UIuserRepository;
    
    public ManagePostCLIView(iPostRepository postRepository)
    {
        UIuserRepository = postRepository;
    }


    public async Task ShowMenuAsync()
    {
        throw new NotImplementedException();
    }
}