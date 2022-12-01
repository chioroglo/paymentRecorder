using Common.Validation.ValidationConstraints;
using Microsoft.AspNetCore.Identity;

namespace PaymentRecorder.Extensions
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            return services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = ApplicationUserValidationConstraints.PasswordMinLength;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;
                
            });
        }
    }
}
