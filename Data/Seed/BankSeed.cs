using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed;

public class BankSeed
{
    public static async Task Seed(EfDbContext dbContext)
    {
        if (!await dbContext.Banks.AnyAsync())
        {
            var banks = new Bank[]
            {
                new Bank
                {
                    Name = "BC MAIB",
                    Code = @"AGRNMD2X"
                },
                new Bank
                {
                    Name = @"MOLDINCONBANK",
                    Code = @"MOLDMD2X"
                },
                new Bank
                {
                    Name = @"VIRGINIA NATIONAL BANK",
                    Code = @"VNBVUS33"
                }
            };

            await dbContext.Banks.AddRangeAsync(banks);

            await dbContext.SaveChangesAsync();
        }
    }
}