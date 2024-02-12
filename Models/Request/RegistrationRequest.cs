namespace newProject.Models;
using System.ComponentModel.DataAnnotations;
using newProject.Models;
public class RegistrationRequest
{
    [Required]
    public UserCreateRequest UserModel { get; set; }

    [Required]
    public ProfileCreateRequest ProfileModel { get; set; }
}