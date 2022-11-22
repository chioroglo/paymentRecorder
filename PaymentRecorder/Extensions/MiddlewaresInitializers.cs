using PaymentRecorder.Middlewares;

namespace PaymentRecorder.Extensions;

public static class MiddlewaresInitializers
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseDbTransactionPerRequest(this IApplicationBuilder app)
    {
        return app.UseMiddleware<DbTransactionMiddleware>();
    }
}