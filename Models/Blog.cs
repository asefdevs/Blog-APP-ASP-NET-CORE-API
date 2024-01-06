namespace newProject.Models;
using newProject.Entities;

public class BlogResponse
{
    public List<Blog> Blogs { get; set; } = null!;

}

public class BlogCreateRequest
{
    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;
}