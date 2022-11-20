using PaymentRecorder.Extensions;

namespace PaymentRecorder
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = WebHostFactory.BuildWithStartup();

            await (await host.MigrateAndSeedDbAsync()).RunAsync();
        }
    }
}