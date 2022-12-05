namespace Common.Jwt
{
    public class JWTConfigurationFromAppsettingsJson
    {
        public string Key { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public double AccessTokenLifetimeMinutes { get; set; }

        public double RefreshTokenLifetimeInDays { get; set; }
    }
}