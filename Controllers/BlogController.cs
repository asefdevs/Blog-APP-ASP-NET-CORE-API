using Microsoft.AspNetCore.Mvc;

namespace newProject.Controllers;
using newProject.Entities;
using System.Collections.Generic;
using newProject.Services;
using newProject.Models;
using System.Threading.Tasks;
using AutoMapper;




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
    public async Task<IActionResult> AllBlogs()
    {
        var blogs = await _blogService.GetAllBlogs();
        return Ok(blogs);    
    }

    [HttpPost]
    [Route("CreateBlog")]
    public async Task<IActionResult> CreateBlog(BlogCreateRequest model)
    {
        var blog = await _blogService.CreateBlog(model);
        return Ok(blog);
    }

}
