﻿namespace Entities;

public class Comment
{
    public int CommentId { get;  set; }
    public string Body { get; set; }

    public Comment(string body)
    {
        Body = body;
    }

  
}