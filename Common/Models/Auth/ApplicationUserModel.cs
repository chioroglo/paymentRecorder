namespace Common.Models.Auth;

public class ApplicationUserModel
{
    public string Username { get; set; }

    public string Email { get; set; }

    public IEnumerable<string> Roles { get; set; }
}