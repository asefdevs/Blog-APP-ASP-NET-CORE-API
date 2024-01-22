namespace newProject.Models;
using System.ComponentModel.DataAnnotations;

public class UserUpdateRequest
{
    public string? UserName { get; set; } 

    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; } 

}