using PaymentRecorder.Middlewares;

namespace PaymentRecorder.Extensions;

public static class MiddlewaresInitializers
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}