namespace newProject.Models;
using newProject.Entities;


public class LoginResponse
{

    public int Id { get; set; }
    public string AccessToken { get; set; } = null!;

    public LoginResponse(User user, string accessToken)
    {
        Id = user.Id;
        AccessToken = accessToken;
    }

}