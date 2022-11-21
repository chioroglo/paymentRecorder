using Service;
using Service.Abstract;

namespace PaymentRecorder.Extensions;

public static class ServicesInitializer
{
    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddTransient<IAgentService, AgentService>();
    }
}