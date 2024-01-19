namespace newProject.Services;
using newProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using newProject.Models;


public interface IUserService
{   
    Task<UserResponse> AllUsers();
    Task<UserCreateRequest> CreateUser(UserCreateRequest userCreateRequest);

}

public class UserService : IUserService
{
    private readonly MyprojectdbContext _context;

    public UserService(MyprojectdbContext context)
    {
        _context = context;
    }

    public async Task<UserResponse> AllUsers()
    {
        var users = await _context.Users.ToListAsync();
        return new UserResponse
        {
            Users = users
        };
    }

    public async Task<UserCreateRequest> CreateUser(UserCreateRequest userCreateRequest)
    {
        var user = CastUserDto(userCreateRequest);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return userCreateRequest;
    }

    private static User CastUserDto(UserCreateRequest userCreateRequest)
    {
        return new User
        {
            UserName = userCreateRequest.UserName,
            Password = userCreateRequest.Password,
            Email = userCreateRequest.Email,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsAdmin = userCreateRequest.IsAdmin,
            IsActive = userCreateRequest.IsActive           
        };
    }
        
        
}