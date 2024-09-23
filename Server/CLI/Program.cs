using CLI.UI;
using FileRepositories;
// using InMemoryRepositories; Add reference to activate CLI-view. 
using RepositoryContracts;

namespace CLI;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting CliApp.cs");
        IUserRepository userRepository = new UserFileRepository(); // CLI: UserInMemoryRepository
        ICommentRepository commentRepository = new CommentFileRepository(); // CLI: CommentInMemoryRepository
        iPostRepository postRepository = new PostFileRepository(); // CLI: CommentInMemoryRepository

        CliApp cliApp = new CliApp(userRepository, commentRepository, postRepository);
        await cliApp.StartAsync();
        


    }
}