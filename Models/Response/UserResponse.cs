namespace newProject.Models;
using newProject.Entities;


public class UserResponse
{

    public int Id { get; set; }
    
    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Country { get; set; }

    public string? Bio { get; set; }

    public bool? IsAdmin { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }


}
