namespace Common.Jwt;

public class RefreshToken
{
    public string Token { get; set; }

    public DateTime ExpirationDate { get; set; }
}