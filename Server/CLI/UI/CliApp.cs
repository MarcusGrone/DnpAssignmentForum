using RepositoryContracts;
using CLI.UI.User;
using CLI.UI.Comment;
using CLI.UI.Post;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository UIuserRepository;
    private readonly ICommentRepository UIcommentRepository;
    private readonly iPostRepository UIpostRepository;

    public CliApp(IUserRepository userRepository,
        ICommentRepository commentRepository, iPostRepository postRepository)
    {
        UIuserRepository = userRepository;
        UIcommentRepository = commentRepository;
        UIpostRepository = postRepository;
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("Forum CLI activated; Please enter:");
            Console.WriteLine("1. Manage users");
            Console.WriteLine("2. Manage posts");
            Console.WriteLine("3. Manage comments");
            Console.WriteLine("4. Exit program");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    var manageUsersView =
                        new ManageUsersCLIView(UIuserRepository);
                    await manageUsersView.ShowMenuAsync();
                    break;
                case "2":
                    var managePostsView =
                        new ManagePostCLIView(UIpostRepository);
                    await managePostsView.ShowMenuAsync();
                    break;
                case "3":
                    var manageCommentsView =
                        new ManageCommentCLIView(UIcommentRepository);
                    await manageCommentsView.ShowMenuAsync();
                    break;
                case "4":
                    Console.WriteLine("Exiting Forum CLI...");
                    break;
                default:
                    Console.WriteLine("Invalid number, please enter 1-5.");
                break;
            }
            Console.WriteLine("Press any key to restat loop");
            Console.ReadKey();
        }
        
    }
}