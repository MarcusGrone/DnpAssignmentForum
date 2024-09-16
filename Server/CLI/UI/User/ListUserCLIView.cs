using RepositoryContracts;

namespace CLI.UI.User;

public class ListUserCLIView
{
    private readonly IUserRepository _userRepository;

    public ListUserCLIView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync()
    {
        var users = _userRepository.GetMany().ToList();

        if (!users.Any())
        {
            Console.WriteLine("Unable to comply, userList is EMPTY");
            return;
        }

        foreach (var user in users)
        {
            Console.WriteLine(
                $"User ID: {user.UserId}, Username: {user.UserName}");
        }


        await Task.CompletedTask;
    }
}