using System.Net;
using Common.Exceptions;
using Common.Models.Error;

namespace PaymentRecorder.Middlewares;

public class ExceptionHandlingMiddleware
{

    private readonly RequestDelegate _next;
    
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            switch (e)
            {
                case EntityValidationException:
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                }
                case OperationCanceledException:
                {
                    return;
                }
                default:
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                }
            }

            await CreateExceptionResponseAsync(context, e);
        }
    }

    private Task CreateExceptionResponseAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsync(
            new ErrorDetails(context.Response.StatusCode, exception.Message).ToString()
        );
    }
}