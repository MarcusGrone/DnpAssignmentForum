using RepositoryContracts;

namespace CLI.UI.User;

public class ManageUsersCLIView
{
    private readonly IUserRepository _userRepository;

    public ManageUsersCLIView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ShowMenuAsync()
    {
        while (true)
        {
            Console.WriteLine("\n UserManagement:");
            Console.WriteLine("1. Create, update or delete user");
            Console.WriteLine("2. See list of users");
            Console.WriteLine("3. Return to main menu");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    var lifeCycleUserCliView = new LifecycleUserCLIView(_userRepository);
                    await lifeCycleUserCliView.ExecuteAsync();
                    break;
                case "2":
                    var listUserCLIView = new ListUserCLIView(_userRepository);
                    await listUserCLIView.ExecuteAsync();
                    break;
                case "3":
                    Console.WriteLine("Returning to main menu.");
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
}