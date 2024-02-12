using newProject.Models;
namespace newProject.Models;

public class ProfileUpdate
{
    public UserUpdateRequest UserModel { get; set; }

    public ProfileCreateRequest ProfileModel { get; set; }
}
