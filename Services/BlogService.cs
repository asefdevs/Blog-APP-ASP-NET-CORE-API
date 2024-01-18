namespace newProject.Services;
using newProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using newProject.Models;




public interface IBlogService
{   
    Task<BlogResponse> AllBlogs();
    Task<BlogCreateRequest> CreateBlog(BlogCreateRequest blogCreateRequest);

}

public class BlogService : IBlogService
{
    private readonly MyprojectdbContext _context;

    public BlogService(MyprojectdbContext context)
    {
        _context = context;
    }

    public async Task<BlogResponse> AllBlogs()
    {
        var blogs = await _context.Blogs.ToListAsync();
        return new BlogResponse
        {
            Blogs = blogs
        };
    }

    public async Task<BlogCreateRequest> CreateBlog(BlogCreateRequest blogCreateRequest)
    {
        var blog = CastBlogDto(blogCreateRequest);
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
        return blogCreateRequest;
    }

    private static Blog CastBlogDto(BlogCreateRequest blogCreateRequest)
    {
        return new Blog
        {
            Title = blogCreateRequest.Title,
            Content = blogCreateRequest.Content,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            UserId = blogCreateRequest.UserId

            
        };
    }
        
        
}
