namespace Common.Dto.Auth;

public class AuthenticationServiceResponseDto
{
    public string Username { get; set; }

    public string Email { get; set; }

    public IEnumerable<string> Roles { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpirationDate { get; set; }

    public string AccessToken { get; set; }

    public DateTime AccessTokenExpirationDate { get; set; }
}