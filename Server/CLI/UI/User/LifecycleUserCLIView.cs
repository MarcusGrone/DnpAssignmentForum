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
        Console.WriteLine("User Management - Create, Update, or Delete:");
        Console.WriteLine("1. Create a new user");
        Console.WriteLine("2. Update an existing user");
        Console.WriteLine("3. Delete a user");
        Console.WriteLine("4. Return to the previous menu");


        while (true)
        {
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await CreateUserAsync();
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

    private async Task CreateUserAsync()
    {
        Console.WriteLine("Enter a username:");
        var username = Console.ReadLine();

        Console.WriteLine("Enter a password:");
        var password = Console.ReadLine();

        var newUser = new Entities.User(username, password);
        
        await _userRepository.AddAsync(newUser);
        
        Console.WriteLine($"User {username}, was created successfully");
        
    }

    private async Task UpdateUserAsync()
    {
        Console.WriteLine("Enter user ID to update user:");
        if (int.TryParse(Console.ReadLine(), out var userId))
        {
            var user = await _userRepository.GetSingleAsync(userId);
            if (user == null)
            {
                Console.WriteLine("Insufficient userId");
                return;
            }
            Console.WriteLine($"Found User: {user.UserName}");
            Console.WriteLine("Enter a new username to Update:");
            var newUsername = Console.ReadLine();
            
            Console.WriteLine("Enter a new password to Update:");
            var newPassword = Console.ReadLine();

            user.UserName = newUsername;
            user.Password = newPassword;
            
            await _userRepository.UpdateAsync(user);

            Console.WriteLine($"User '{newUsername}' updated successfully.");

        }
        else
        {
            Console.WriteLine("Unable to comply, userId NOT FOUND.");
        }
    }

    private async Task DeleteUserAsync()
    {
        Console.WriteLine("Please enter userId you want to DELETE");
        if (int.TryParse(Console.ReadLine(), out var userId))
        {
            await _userRepository.DeleteAsync(userId);
            Console.WriteLine($"userId: {userId}, has successfully been deleted.");
        }
        else
        {
            Console.WriteLine("Unable to comply, userId NOT FOUND");
        }
    }
}