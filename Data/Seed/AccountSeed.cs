using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed;

public class AccountSeed
{
    public static async Task Seed(EfDbContext dbContext)
    {
        if (!await dbContext.Accounts.AnyAsync())
        {
            var accounts = new Account[]
            {
                new Account
                {
                    AccountCode = @"2251617442",
                    Agent = await dbContext.Agents.FirstAsync(e => e.FiscalCode == 2003500019060),
                    Bank = await dbContext.Banks.FirstAsync(e => e.Name == "BC MAIB")
                },
                new Account
                {
                    AccountCode = @"1488954321",
                    Agent = await dbContext.Agents.FirstAsync(e => e.FiscalCode == 1016600001027),
                    Bank = await dbContext.Banks.FirstAsync(e => e.Name == "BC MAIB")
                },
                new Account
                {
                    AccountCode = @"1422948543",
                    Agent = await dbContext.Agents.FirstAsync(e => e.FiscalCode == 1016600001027),
                    Bank = await dbContext.Banks.FirstAsync(e => e.Name == "VIRGINIA NATIONAL BANK")
                },
                new Account
                {
                    AccountCode = @"9922418421",
                    Agent = await dbContext.Agents.FirstAsync(e => e.FiscalCode == 1400450042010),
                    Bank = await dbContext.Banks.FirstAsync(e => e.Name == @"MOLDINCONBANK")
                }
            };

            await dbContext.Accounts.AddRangeAsync(accounts);

            await dbContext.SaveChangesAsync();
        }
    }
}