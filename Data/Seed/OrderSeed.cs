using Domain;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed;

public class OrderSeed
{
    public static async Task Seed(EfDbContext dbContext)
    {
        if (!await dbContext.Orders.AnyAsync())
        {
            Order[] orders = new Order[]
            {
                new Order
                {
                    Number = 441,
                    Amount = 15402.11m,
                    CurrencyCode = CurrencyCode.MDL,
                    Destination = "Buying of tobacco goods for conference in Moldova",
                    IssuerAccount = await dbContext.Accounts.FirstAsync(e => e.AccountCode == @"2251617442"),
                    BeneficiaryAccount = await dbContext.Accounts.FirstAsync(e => e.AccountCode == @"1488954321"),
                    EmissionDate = DateTime.UtcNow,
                    IssueDate = DateTime.UtcNow,
                    ExecutionDate = DateTime.UtcNow,
                    Timezone = "Europe/Chisinau"
                },
                new Order
                {
                    Number = 442,
                    Amount = 45021.40m,
                    CurrencyCode = CurrencyCode.USD,
                    Destination = "Wholesale purchase of Parliament Silver Blue from US from port HQ Virginia",
                    IssuerAccount = await dbContext.Accounts.FirstAsync(e => e.AccountCode == @"9922418421"),
                    BeneficiaryAccount = await dbContext.Accounts.FirstAsync(e => e.AccountCode == @"1422948543"),
                    EmissionDate = DateTime.UtcNow.AddMinutes(-20),
                    IssueDate = DateTime.UtcNow.AddMinutes(-21),
                    ExecutionDate = DateTime.UtcNow,
                    Timezone = "Europe/Chisinau"
                }
            };

            await dbContext.Orders.AddRangeAsync(orders);

            await dbContext.SaveChangesAsync();
        }
    }
}