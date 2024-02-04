using Microsoft.AspNetCore.Mvc;
namespace newProject.Controllers;
using newProject.Entities;
using newProject.Services;
using newProject.Models;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Security.Claims;
using newProject.Exceptions;




[ApiController]
[Route("[controller]")]

public class BlogController:ControllerBase
{
    public readonly MyprojectdbContext _context;
    private readonly IBlogService _blogService;


    private readonly IMapper _mapper;

    
    public BlogController(MyprojectdbContext context, IBlogService blogService, IMapper mapper)
    {
        _context = context;
        _blogService = blogService;
        _mapper =  mapper;


    }


    [HttpGet]
    [Route("GetBlogs")]
    public async Task<IActionResult> AllBlogs(string searchTerm = null, int? categoryId = null)
    {
        try
        {
            var blogs = await _blogService.GetAllBlogs(searchTerm, categoryId);
            return Ok(blogs);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet]
    [Route("GetBlogById/{id}")]
    public async Task<IActionResult> GetBlogById(int id)
    {
        try
        {
            var blog = await _blogService.GetBlogById(id);
            return Ok(blog);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize]
    [Route("CreateBlog")]
    public async Task<IActionResult> CreateBlog(BlogCreateRequest model)
    {
        try 
        {   ClaimsPrincipal user =  HttpContext.User;
            var userId = ClaimsHelper.RequestedUser(user);

            var blog = await _blogService.CreateBlog(model,userId.Value);
            return Ok(blog);
        }
        catch (NotFoundException ex)
        {
            return StatusCode(404, new {message = ex.Message});
        }
    }

    [HttpPut]
    [Authorize]
    [Route("UpdateBlog/{id}")]
    public async Task<IActionResult> UpdateBlog(int id,BlogUpdateRequest model)
    {
        try 
        {   ClaimsPrincipal user =  HttpContext.User;
            var blog = await _blogService.UpdateBlog(id, user, model);
            return Ok(blog);
        }
       catch (ForbbidenAccessException ex)
        {
            return StatusCode(403, new {message = ex.Message});
        }
        catch (NotFoundException ex)
        {
            return StatusCode(404, new {message = ex.Message});
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("DeleteBlog/{id}")]
    public async Task<IActionResult> DeleteBlog(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        var author = blog.UserId;
        ClaimsPrincipal user =  HttpContext.User;
        var userId = ClaimsHelper.RequestedUser(user);
        if (userId != author)
        {
            return StatusCode(403, new {message = "You are not author of this blog"});
        }

        if (blog == null)
        {
            return NotFound(new { message = "Blog not found" });
        }

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete]
    [Route("DeleteAllBlogs")]
    public async Task<IActionResult> DeleteAllBlogs()
    {
        
        {
            var blogs = await _context.Blogs.ToListAsync();

            if (blogs == null || !blogs.Any())
            {
                return NotFound("No blogs found to delete.");
            }

            _context.Blogs.RemoveRange(blogs);
            await _context.SaveChangesAsync();

            return Ok("All blogs deleted successfully.");
        }
    }
}
