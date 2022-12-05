namespace Common.Jwt;

public class AccessToken
{
    public string Token { get; set; }

    public DateTime ExpirationDate { get; set; }
}