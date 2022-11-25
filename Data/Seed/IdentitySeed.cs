using System.Diagnostics.CodeAnalysis;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed;

public class IdentitySeed
{
    public static async Task Seed(EfDbContext db)
    {
        if (!await db.Roles.AnyAsync())
        {
            IdentityRole[] identityRoles = new IdentityRole[]
            {
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Accountant",
                    NormalizedName = "Accountant".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Manager",
                    NormalizedName = "Manager".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
                ,
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "System Administrator",
                    NormalizedName = "System Administrator".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            };

            await db.Roles.AddRangeAsync(identityRoles);

            await db.SaveChangesAsync(true);
        }
    }
}