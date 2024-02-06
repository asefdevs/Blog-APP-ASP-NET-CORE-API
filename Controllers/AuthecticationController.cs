using Microsoft.AspNetCore.Mvc;
namespace newProject.Controllers;
using newProject.Entities;
using newProject.Services;
using newProject.Models;
using System.Threading.Tasks;
using AutoMapper;
using System.Security.Claims;



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
        try
        {
            var user = await _authenticationService.Login(model);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        
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

    [HttpPost]
    [Route("GenerateTotp")]
    public async Task<IActionResult> GenerateTotp(GenerateTOTPRequest model)
    {
        try
        {
            var totp = await _authenticationService.GenerateTotp(model);
            return Ok(totp);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    [Route("VerifyTotp")]
    public async Task<IActionResult> VerifyTotp(VerifyTotpRequest model)
    {

        try
        {
            var result = await _authenticationService.VerifyTotp(model);
            if (result)
            {
                return Ok(new { message = "TOTP is verified"});
            }
            return BadRequest(new { message = "TOTP is not verified"});
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
        
    
    }
    

}






