using Domain;
using Microsoft.AspNetCore.Identity;
using Service;
using Service.Abstract;
using Service.Utils;

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
        services.AddTransient<ITransactionService, TransactionService>();
        services.AddTransient<IPasswordHasher<ApplicationUser>, PasswordHasherHmacSha512>();
    }
}