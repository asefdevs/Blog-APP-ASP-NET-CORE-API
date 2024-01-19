namespace newProject.Models;
using newProject.Entities;
using System;
using System.Collections.Generic;


public class BlogResponse
{

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }

    public UserResponse User { get; set; } = null!;


}

public class BlogCreateRequest
{
    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }
}