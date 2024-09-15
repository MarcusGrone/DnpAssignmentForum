using RepositoryContracts;

namespace CLI.UI.User;

public class ManageUsersCLIView
{
    
    private readonly IUserRepository UIpostRepository;
    
    public ManageUsersCLIView(IUserRepository userRepository)
    {
        UIpostRepository = userRepository;
    }

    public async Task ShowMenuAsync()
    {
        throw new NotImplementedException();
    }
}