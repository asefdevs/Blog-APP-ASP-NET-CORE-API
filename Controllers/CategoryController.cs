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
public class CategoryController:ControllerBase
{
    public readonly MyprojectdbContext _context;
    private readonly IHelperService _helperService;
    private readonly IMapper _mapper;

    public CategoryController(MyprojectdbContext context, IHelperService helperService, IMapper mapper)
    {
        _context = context;
        _helperService = helperService;
        _mapper =  mapper;
    }

    [HttpGet]
    [Route("GetCategories")]
    public async Task<IActionResult> AllCategories()
    {
        try
        {
            var categories = await _helperService.AllCategories();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}