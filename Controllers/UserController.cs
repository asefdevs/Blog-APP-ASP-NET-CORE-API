using Microsoft.AspNetCore.Mvc;

namespace newProject.Controllers;
using newProject.Entities;
using System.Collections.Generic;
using newProject.Services;
using newProject.Models;


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
    [HttpGet]
    [Route("GetUsers")]
    public async Task<IActionResult> AllUsers()
    {
        var users = await _userService.AllUsers();
        return Ok(users);    
}

}

