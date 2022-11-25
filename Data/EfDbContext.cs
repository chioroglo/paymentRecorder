using System.Reflection;
using Data.EntityConfiguration;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public sealed class EfDbContext : IdentityDbContext<ApplicationUser>
{

    // TODO add indexes in entity configuration
    public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<Agent> Agents { get; set; }

    public DbSet<Bank> Banks { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Assembly assembly = typeof(EntityConfigurationAssemblyMarker).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}