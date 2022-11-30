using Service;
using Service.Abstract;

namespace PaymentRecorder.Extensions;

public static class ServicesInitializer
{
    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddTransient<IAgentService, AgentService>();
        services.AddTransient<IBankService, BankService>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IOrderService, OrderService>();
    }
}