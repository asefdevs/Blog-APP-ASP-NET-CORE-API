namespace newProject.Models;
using newProject.Entities;




public class UserCreateRequest
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? IsAdmin { get; set; }

    public bool? IsActive { get; set; }


}