using Domain;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed;

public class TransactionSeed
{
    public static async Task Seed(EfDbContext dbContext)
    {
        if (!await dbContext.Transactions.AnyAsync())
        {
            Transaction[] transactions = new Transaction[]
            {
                new Transaction
                {
                    Order = await dbContext.Orders.FirstAsync(e =>
                        e.Destination == "Wholesale purchase of Parliament Silver Blue from US from port HQ Virginia"),
                    OrderId = (await dbContext.Orders.FirstAsync(e =>
                        e.Destination == "Wholesale purchase of Parliament Silver Blue from US from port HQ Virginia")).Id,
                    TransactionType = TransactionType.Regular,
                    TransactionState = TransactionState.Completed
                },
                new Transaction
                {
                    Order = await dbContext.Orders.FirstAsync(e =>
                        e.Destination == "Buying of tobacco goods for conference in Moldova"),
                    OrderId = (await dbContext.Orders.FirstAsync(e =>
                        e.Destination == "Buying of tobacco goods for conference in Moldova")).Id,
                    TransactionType = TransactionType.Regular,
                    TransactionState = TransactionState.Completed
                }
            };

            await dbContext.Transactions.AddRangeAsync(transactions);

            await dbContext.SaveChangesAsync();
        }
    }
}