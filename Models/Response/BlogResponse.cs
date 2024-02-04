namespace newProject.Models;
using System;
using newProject.Helpers.Dto;

public class BlogResponse
{

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public UserResponse? User { get; set; }

    public CategoryDto? Category { get; set; }
        
}