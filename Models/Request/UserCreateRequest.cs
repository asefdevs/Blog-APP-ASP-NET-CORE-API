namespace newProject.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;





public class UserCreateRequest
{
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(50, ErrorMessage = "Username can't be longer than 50 characters")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password can't be longer than 8 characters")]
    public string Password { get; set; } = null!;

    public string Password2 { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } 

    public bool? IsAdmin { get; set; }

    public bool? IsActive { get; set; }


}