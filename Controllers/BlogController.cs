using Microsoft.AspNetCore.Mvc;

namespace newProject.Controllers;
using newProject.Entities;
using System.Collections.Generic;
using newProject.Services;
using newProject.Models;



[ApiController]
[Route("[controller]")]

public class BlogController:ControllerBase
{
    public readonly MyprojectdbContext _context;
    private readonly IBlogService _blogService;
    
    public BlogController(MyprojectdbContext context, IBlogService blogService)
    {
        _context = context;
        _blogService = blogService;
    }


    [HttpGet]
    [Route("GetBlogs")]
    public async Task<IActionResult> AllBlogs()
    {
        var blogs = await _blogService.AllBlogs();
        return Ok(blogs);    
    }

    [HttpPost]
    [Route("CreateBlog")]
    public async Task<IActionResult> CreateBlog(BlogCreateRequest blogCreateRequest)
    {
        var blog = await _blogService.CreateBlog(blogCreateRequest);
        return CreatedAtAction(nameof(AllBlogs),blog);
    }
 
}
