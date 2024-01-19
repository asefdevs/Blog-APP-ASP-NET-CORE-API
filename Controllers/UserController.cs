using Microsoft.AspNetCore.Mvc;

namespace newProject.Controllers;
using newProject.Entities;
using System.Collections.Generic;
using newProject.Services;
using newProject.Models;
using AutoMapper;


[ApiController]
[Route("[controller]")]
public class UserController:ControllerBase
{
    public readonly MyprojectdbContext _context;
    private readonly IUserService _userService;

    private readonly IMapper _mapper;



    public UserController(MyprojectdbContext context, IUserService userService, IMapper mapper)
    {
        _context = context;
        _userService = userService;
        _mapper =  mapper;
    }

    [HttpGet]
    [Route("GetUsers")]
    public async Task<IActionResult> AllUsers()
    {
        var users = await _userService.AllUsers();
        return Ok(users);
    }

    [HttpPost]
    [Route("CreateUser")]
    public async Task<IActionResult> CreateUser(UserCreateRequest model)
    {
        var user = await _userService.CreateUser(model);
        return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);

    }

}
