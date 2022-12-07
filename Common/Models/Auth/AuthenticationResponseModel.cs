namespace Common.Models.Auth;

public class AuthenticationResponseModel
{
    public string Username { get; set; }

    public string Email { get; set; }

    public IEnumerable<string> Roles { get; set; }

    public string AccessToken { get; set; }

    public DateTime AccessTokenExpirationDate { get; set; }
}