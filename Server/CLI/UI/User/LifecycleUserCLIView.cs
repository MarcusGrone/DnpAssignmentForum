using RepositoryContracts;
using Entities;

namespace CLI.UI.User;

public class LifecycleUserCLIView
{
    private readonly IUserRepository _userRepository;

    public LifecycleUserCLIView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync()
    {
        while (true)
        {
            Console.WriteLine("User Management - Create, Update, or Delete:");
            Console.WriteLine("1. Create a new user");
            Console.WriteLine("2. Update an existing user");
            Console.WriteLine("3. Delete a user");
            Console.WriteLine("4. Return to the previous menu");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                   
                    break;
                case "2":
                    await UpdateUserAsync();
                    break;
                case "3":
                    await DeleteUserAsync();
                    break;
                case "4":
                    Console.WriteLine("Returning to previous menu.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

  /*  private async Task CreateUserAsync()
    {
        Console.WriteLine("Enter a username:");
        var username = Console.ReadLine();

        Console.WriteLine("Enter a password:");
        var password = Console.ReadLine();

        var newUser = new Entities.User();

        await _userRepository.AddAsync(newUser);

        Console.WriteLine($"User {username}, was created successfully");
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    } */

    private async Task UpdateUserAsync()
    {
        Console.WriteLine("Enter user ID to update:");
        if (int.TryParse(Console.ReadLine(), out var userId))
        {
            try
            {
                var user = await _userRepository.GetSingleAsync(userId);
                if (user != null)
                {
                    Console.WriteLine($"Found User: {user.UserName}");
                    Console.WriteLine("Enter a new username:");
                    var newUsername = Console.ReadLine();

                    Console.WriteLine("Enter a new password:");
                    var newPassword = Console.ReadLine();

                    user.UserName = newUsername;
                    user.Password = newPassword;

                    await _userRepository.UpdateAsync(user);

                    Console.WriteLine(
                        $"User '{newUsername}' updated successfully.");
                }
                else
                {
                    Console.WriteLine($"User with ID '{userId}' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to comply: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid user ID format.");
        }

        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }

    private async Task DeleteUserAsync()
    {
        Console.WriteLine("Enter user ID to delete:");
        if (int.TryParse(Console.ReadLine(), out var userId))
        {
            try
            {
                var user = await _userRepository.GetSingleAsync(userId);
                if (user != null)
                {
                    await _userRepository.DeleteAsync(userId);
                    Console.WriteLine(
                        $"User with ID '{userId}' has been successfully deleted.");
                }
                else
                {
                    Console.WriteLine($"User with ID '{userId}' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to comply: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid user ID format.");
        }

        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }
}