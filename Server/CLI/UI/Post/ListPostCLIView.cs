﻿using RepositoryContracts;

namespace CLI.UI.Post;

public class ListPostCLIView
{
    private readonly IPostRepository _postRepository;

    public ListPostCLIView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task ShowAllPostsAsync()
    {
        var posts = _postRepository.GetMany().ToList();

        if (!posts.Any())
        {
            Console.WriteLine("No posts available.");
        }
        else
        {
            foreach (var post in posts)
            {
                Console.WriteLine(
                    $"Post ID: {post.PostId}, Title: {post.Title}, Body: {post.Body}");
            }
        }

        await Task.CompletedTask;
    }
}