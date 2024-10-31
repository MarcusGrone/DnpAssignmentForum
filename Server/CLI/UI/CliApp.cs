using RepositoryContracts;
using CLI.UI.User;
using CLI.UI.Comment;
using CLI.UI.Post;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;

    public CliApp(IUserRepository userRepository,
        ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("\nForum CLI activated; Please enter:");
            Console.WriteLine("1. Manage users");
            Console.WriteLine("2. Manage posts");
            Console.WriteLine("3. Manage comments");
            Console.WriteLine("4. Exit program");
            Console.WriteLine("5. Insert dummy data");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    var manageUsersView =
                        new ManageUsersCLIView(_userRepository);
                    await manageUsersView.ShowMenuAsync();
                    break;
                case "2":
                    var managePostsView =
                        new ManagePostCLIView(_postRepository);
                    await managePostsView.ShowMenuAsync();
                    break;
                /*case "3":
                    var manageCommentsView =
                        new ManageCommentCLIView(_commentRepository);
                    await manageCommentsView.ShowMenuAsync();
                    break;*/
                case "4":
                    Console.WriteLine("Exiting Forum CLI...");
                    break;
              /*  case "5":
                    Console.WriteLine("Inserting dummy data...");
                    _userRepository.InitializeDummyData();
                    _postRepository.InitializeDummyData();
                    _commentRepository.InitializeDummyData();
                    Console.WriteLine("Dummy data generated.");
                    break;*/
                
                default:
                    Console.WriteLine("Invalid number, please enter 1-5.");
                break;
            }
            Console.WriteLine("\nPress any key to restart Main menu");
            Console.ReadKey();
        }
        
    }
}