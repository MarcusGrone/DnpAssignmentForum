﻿namespace Entities;

public class Post
{
    public int PostId { get;  set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public Post( string title, string body)
    {
        Title = title;
        Body = body;
    }
}