namespace PaymentRecorder;

public class WebHostFactory
{
    public static IHost BuildWithStartup()
    {
        return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => { builder.UseStartup<Startup>(); })
            .Build();
    }
}