namespace newProject.Models;
using newProject.Entities;


public class UserResponse
{

    public List<User> Users { get; set; } = null!;

}

public class UserCreateRequest
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool? IsAdmin { get; set; }

    public bool? IsActive { get; set; }

}