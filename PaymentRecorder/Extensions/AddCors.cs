namespace PaymentRecorder.Extensions;

public static class CorsPolicyConfiguration
{
    public static void AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder
                        .WithOrigins(configuration["ReactApplicationAddress"])
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                        .AllowCredentials();
                });
        });
    }
}