using RepositoryContracts;

namespace CLI.UI.Post

{
    public class ManagePostCLIView
    {
        private readonly iPostRepository _postRepository;

        public ManagePostCLIView(iPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task ShowMenuAsync()
        {
            Console.WriteLine("Post Management:");
            Console.WriteLine("1. Add Post");
            Console.WriteLine("2. List Posts");
            Console.WriteLine("3. Single Posts");
            Console.WriteLine("4. Return to main menu");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    var lifecyclePostCLIView = new LifecyclePostCLIView(_postRepository);
                    await lifecyclePostCLIView.ExecuteAsync();
                    break;
                case "2":
                    var listPostCLIView = new ListPostCLIView(_postRepository);
                    await listPostCLIView.ShowAllPostsAsync();
                    break;
                case "3":
                    var singlePostCLIView =
                        new SinglePostCLIView(_postRepository);
                    await singlePostCLIView.ShowSinglePostAsync();
                    break;
                case "4":
                    Console.WriteLine("Returning to main menu.");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        
    }
}
