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
public class CommentController:ControllerBase
{
    public readonly MyprojectdbContext _context;

    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentController(MyprojectdbContext context, ICommentService commentService, IMapper mapper)
    {
        _context = context;
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetComments/{blogId}")]
    public async Task<IActionResult> AllComments(int blogId)
    {
        try
        {
            ClaimsPrincipal user = HttpContext.User;
            var userId = ClaimsHelper.RequestedUser(user);

            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null)
            {
                return NotFound(new { message = "Blog not found." });
            }

            var comments = await _commentService.AllComments(blogId);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    [Route("CreateComment")]
    [Authorize]
    public async Task<IActionResult> CreateComment(CommentCreateRequest model)
    {
        try
        {
            ClaimsPrincipal user = HttpContext.User;
            var userId = ClaimsHelper.RequestedUser(user);

            var blog = await _context.Blogs.FindAsync(model.BlogId);
            if (blog == null)
            {
                return NotFound(new { message = "Blog not found." });
            }

            var comment = await _commentService.CreateComment(model, userId.Value);
            return Ok(comment);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    [HttpDelete]
    [Authorize]
    [Route("DeleteComment/{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        var author = comment.UserId;
        ClaimsPrincipal user = HttpContext.User;
        var userId = ClaimsHelper.RequestedUser(user);
        if (userId != author)
        {
            return StatusCode(403, new { message = "You are not author of this comment" });
        }

        if (comment == null)
        {
            return NotFound(new { message = "Comment not found" });
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Comment deleted successfully" });
    }
}



