using Microsoft.AspNetCore.Mvc;

namespace newProject.Controllers;
using newProject.Entities;
using System.Collections.Generic;
using newProject.Services;
using newProject.Models;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;




[ApiController]
[Route("[controller]")]

public class BlogController:ControllerBase
{
    public readonly MyprojectdbContext _context;
    private readonly IBlogService _blogService;

    private readonly IMapper _mapper;

    
    public BlogController(MyprojectdbContext context, IBlogService blogService,IMapper mapper)
    {
        _context = context;
        _blogService = blogService;
        _mapper =  mapper;

    }


    [HttpGet]
    [Route("GetBlogs")]
    [Authorize]
    public async Task<IActionResult> AllBlogs()
    {
        var blogs = await _blogService.GetAllBlogs();
        return Ok(blogs);    
    }

    [HttpGet]
    [Route("GetBlogById/{id}")]
    public async Task<IActionResult> GetBlogById(int id)
    {
        var blog = await _blogService.GetBlogById(id);
        if (blog == null)
        {
            return NotFound(new { message = "Blog not found" });
        }
        return Ok(blog);
    }

    [HttpPost]
    [Route("CreateBlog")]
    public async Task<IActionResult> CreateBlog(BlogCreateRequest model)
    {
        var blog = await _blogService.CreateBlog(model);
        return Ok(blog);
    }

    [HttpPut]
    [Route("UpdateBlog/{id}")]
    public async Task<IActionResult> UpdateBlog(int id,BlogUpdateRequest model)
    {
        var blog = await _blogService.UpdateBlog(id,model);
        if (blog == null)
        {
            return NotFound(new { message = "Blog not found" });
        }
        return Ok(blog);
    }

    [HttpDelete]
    [Route("DeleteBlog/{id}")]
    public async Task<IActionResult> DeleteBlog(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);

        if (blog == null)
        {
            return NotFound(new { message = "Blog not found" });
        }

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
