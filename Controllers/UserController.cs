using Microsoft.AspNetCore.Mvc;

namespace newProject.Controllers;
using newProject.Entities;
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

    [HttpPut]
    [Route("UpdateUser/{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserUpdateRequest model)
    {
        var user = await _userService.UpdateUser(id, model);
        return Ok(user);
    }

    [HttpGet]
    [Route("GetUserById/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        return Ok(user);
    }

}
