using System.ComponentModel.DataAnnotations;
using Data;
using static Common.Exceptions.ExceptionMessages.ValidationExceptionMessages;

namespace PaymentRecorder.Middlewares;

public class IfMatchHeaderChecker
{
    private readonly RequestDelegate _next;

    public IfMatchHeaderChecker(RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(HttpContext httpContext, EfDbContext db)
    {
        var methodOfOngoingRequest = httpContext.Request.Method;
        
        var methodsThatRequireIfMatchHeaderChecking = new string[]
        {
            HttpMethod.Put.Method,
            HttpMethod.Patch.Method,
            HttpMethod.Delete.Method
        };

        if (Array.Exists(methodsThatRequireIfMatchHeaderChecking, el => el == methodOfOngoingRequest))
        {
            if (!httpContext.Request.Headers.ContainsKey("If-Match"))
            {
                throw new ValidationException(ThisMethodRequiresHttpHeader("If-Match"));
            }

            
        }

        await _next(httpContext);
    }
}