using Domain;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed;

public class AgentSeed
{
    public static async Task Seed(EfDbContext dbContext)
    {
        if (!await dbContext.Agents.AnyAsync())
        {
            Agent[] agents = new Agent[]
            {
                new Agent
                {
                    Name = @"Alexandr Chioroglo",
                    TypeId = LegalAgentType.Physical,
                    FiscalCode = 2003500019060
                },
                new Agent
                {
                    Name = @"Test SRL",
                    TypeId = LegalAgentType.Juridical,
                    FiscalCode = 1400450042010
                },
                new Agent
                {
                    Name = @"Philip Morris Sales & Marketing SRL",
                    TypeId = LegalAgentType.Juridical,
                    FiscalCode = 1016600001027
                }
            };

            await dbContext.Agents.AddRangeAsync(agents);

            await dbContext.SaveChangesAsync();
        }
    }
}