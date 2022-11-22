using Data;
using Microsoft.EntityFrameworkCore;

namespace PaymentRecorder.Middlewares
{
    public class DbTransactionMiddleware
    {
        private readonly RequestDelegate _next;

        public DbTransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext,EfDbContext db)
        {

            if (httpContext.Request.Method == HttpMethod.Get.Method)
            {
                await _next(httpContext);
                return;
            }

            await using (var transaction = await db.Database.BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead))
            {
                await _next(httpContext);

                await db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
    }
}
