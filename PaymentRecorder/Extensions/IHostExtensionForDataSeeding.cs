using Data;
using Data.Seed;

namespace PaymentRecorder.Extensions
{
    public static class IHostExtensionForDataSeeding
    {
        public static async Task<IHost> MigrateAndSeedDbAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<EfDbContext>())
                {
                    try
                    {
                        await SeedFacade.SeedData(context);
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                    
                }
            }

            return host;
        }
    }
}
