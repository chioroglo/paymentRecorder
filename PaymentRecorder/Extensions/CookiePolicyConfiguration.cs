using Microsoft.AspNetCore.CookiePolicy;

namespace PaymentRecorder.Extensions
{
    public static class CookiePolicyConfiguration
    {
        public static IServiceCollection ConfigureCookiePolicy(this IServiceCollection services)
        {
            return services.Configure<CookiePolicyOptions>(options => { });
        }
    }
}