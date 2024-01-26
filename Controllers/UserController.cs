using Microsoft.AspNetCore.Mvc;

namespace newProject.Controllers;
using newProject.Entities;
using newProject.Services;
using newProject.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;

[ApiController]
[Route("[controller]")]
public class UserController:ControllerBase
{
    public readonly MyprojectdbContext _context;
    private readonly IUserService _userService;



    public UserController(MyprojectdbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [HttpPut]
    [Route("UpdateUser/{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserUpdateRequest model)
    {
        try
        {
            ClaimsPrincipal user =  HttpContext.User;
            var updatedUser = await _userService.UpdateUser(id, user, model);
            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [Route("GetUserById/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [Route("MyProfile")]
    [Authorize]
    public async Task<IActionResult> MyProfile()
    {
        try
        {
            ClaimsPrincipal user =  HttpContext.User;
            var userProfile = await _userService.MyProfile(User);
            return Ok(userProfile);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}