namespace newProject.Models;
using newProject.Entities;

public class UserUpdateRequest
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;




}