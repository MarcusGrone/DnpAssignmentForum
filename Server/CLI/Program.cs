using CLI.UI;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting CliApp.cs");
        IUserRepository userRepository = new UserInMemoryRepository();
        ICommentRepository commentRepository = new CommentInMemoryRepository();
        iPostRepository postRepository = new PostInMemoryRepository();

        CliApp cliApp = new CliApp(userRepository, commentRepository, postRepository);
        await cliApp.StartAsync();




    }
}