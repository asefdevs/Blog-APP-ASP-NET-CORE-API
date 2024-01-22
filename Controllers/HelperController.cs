using Microsoft.AspNetCore.Mvc;
namespace newProject.Controllers;
using newProject.Entities;
using newProject.Services;

[ApiController]
[Route("[controller]")]

public class HelperController:ControllerBase
{
    public  readonly MyprojectdbContext _context;
    private readonly IHelperService _helperService;

    public HelperController(MyprojectdbContext context, IHelperService helperService)
    {
        _context = context;
        _helperService = helperService;
    }

    [HttpGet]
    [Route("AllUsers")]
    public async Task<IActionResult> AllUsers()
    {
        var users = await _helperService.AllUsers();
        return Ok(users);
    }
}

