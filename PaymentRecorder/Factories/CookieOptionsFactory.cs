namespace PaymentRecorder.Factories;

public static class CookieOptionsFactory
{
    public static CookieOptions CreateOptionsForTokenCookie(DateTime expires)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = expires,
            SameSite = SameSiteMode.None
        };
    }
}