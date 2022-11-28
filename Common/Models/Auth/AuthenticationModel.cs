namespace Common.Models.Auth;

public class AuthenticationModel
{
    public string Username { get; set; }

    public string Email { get; set; }

    public IEnumerable<string> Roles { get; set; }

    public string Token { get; set; }

    public DateTime ExpiresOn { get; set; }
}