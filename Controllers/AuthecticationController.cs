using Microsoft.AspNetCore.Mvc;
namespace newProject.Controllers;
using newProject.Entities;
using newProject.Services;
using newProject.Models;



[ApiController]
[Route("[controller]")]

public class AuthenticationController:ControllerBase
{
    public  readonly MyprojectdbContext _context;
    private readonly IAuthenticationService _authenticationService;


    public AuthenticationController(MyprojectdbContext context, IAuthenticationService authenticationService)
    {
        _context = context;
        _authenticationService = authenticationService;

    }

    [HttpPost]
    [Route("Login")]

    public async Task<IActionResult> Login(LoginRequest model)
    {
        var user = await _authenticationService.Login(model);
        if (user == null)
        {
            return BadRequest(new { message = "Username or Password is incorrect"});
        }
        return Ok(user);
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(UserCreateRequest model)
    {
        try 
        {
            var user = await _authenticationService.CreateUser(model);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    

}






