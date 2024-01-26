using System.Security.Claims;

public static class ClaimsHelper
{
    public static int? RequestedUser(ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst("UserId");

        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }

        return null;
    }
}