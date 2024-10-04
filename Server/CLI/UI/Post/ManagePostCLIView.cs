using RepositoryContracts;

namespace CLI.UI.Post

{
    public class ManagePostCLIView
    {
        private readonly IPostRepository _postRepository;

        public ManagePostCLIView(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task ShowMenuAsync()
        {
            while (true)
            {
                Console.WriteLine("\nPost Management:");
                Console.WriteLine("1. Create, update or delete Post");
                Console.WriteLine("2. See list of all Posts");
                Console.WriteLine("3. See single Posts");
                Console.WriteLine("4. Return to main menu");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        var lifecyclePostCLIView =
                            new LifecyclePostCLIView(_postRepository);
                        await lifecyclePostCLIView.ExecuteAsync();
                        break;
                    case "2":
                        var listPostCLIView =
                            new ListPostCLIView(_postRepository);
                        await listPostCLIView.ShowAllPostsAsync();
                        break;
                    case "3":
                        var singlePostCLIView =
                            new SinglePostCLIView(_postRepository);
                        await singlePostCLIView.ShowSinglePostAsync();
                        break;
                    case "4":
                        Console.WriteLine("Returning to main menu.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}