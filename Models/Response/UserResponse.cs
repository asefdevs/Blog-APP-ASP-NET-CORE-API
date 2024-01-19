namespace newProject.Models;
using newProject.Entities;


public class UserResponse
{

    public int Id { get; set; }
    
    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? IsAdmin { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }


}
