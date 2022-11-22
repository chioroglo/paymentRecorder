using PaymentRecorder.Middlewares;

namespace PaymentRecorder.Extensions;

public static class MiddlewaresInitializers
{
    public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseDbTransactionPerRequest(this IApplicationBuilder app)
    {
        return app.UseMiddleware<DbTransactionMiddleware>();
    }

    public static IApplicationBuilder UseVersionHeaderChecker(this IApplicationBuilder app)
    {
        return app.UseMiddleware<IfMatchHeaderChecker>();
    }
}