﻿using System.Formats.Tar;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : iPostRepository
{
    private readonly List<Post> posts;

    public PostInMemoryRepository()
    {
        posts = new List<Post>();
        InitializeDummyData();
    }

    public Task<Post> AddAsync(Post post)
    {
        post.PostId = posts.Any()
                ? posts.Max(p => p.PostId) + 1
                : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.PostId == post.PostId);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.PostId}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int postId)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.PostId == postId);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{postId}' not found");
        }

        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int postId)
    {
        Post? postToFind = posts.SingleOrDefault(p => p.PostId == postId);
        if (postToFind is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{postId}' not found");
        }
        return Task.FromResult(postToFind);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
    
    public void InitializeDummyData()
    {
        var posts = new List<Post>
        {
            new Post(1, "First Post", "This is the body of the first post."),
            new Post(2, "Second Post", "This is the body of the second post."),
            new Post(3, "Third Post", "This is the body of the third post.")
        };
    }
    
}