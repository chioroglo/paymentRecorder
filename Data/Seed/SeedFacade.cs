using Microsoft.EntityFrameworkCore;

namespace Data.Seed;

public class SeedFacade
{
    public static async Task SeedData(EfDbContext dbContext)
    {
        await dbContext.Database.MigrateAsync();

        await AgentSeed.Seed(dbContext);
        await BankSeed.Seed(dbContext);
        await AccountSeed.Seed(dbContext);
        await OrderSeed.Seed(dbContext);
        await TransactionSeed.Seed(dbContext);
        await IdentitySeed.Seed(dbContext);
    }
}