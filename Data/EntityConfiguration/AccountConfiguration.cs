using Common.Validation.ValidationConstraints;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfiguration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(nameof(Account));

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.AccountCode)
            .IsUnique();

        builder.Property(e => e.Version)
            .IsConcurrencyToken()
            .HasDefaultValueSql(UtilSqlCommands.SqlServerNewGuidCommand);

        builder.HasMany(e => e.IncomingOrders)
            .WithOne(e => e.BeneficiaryAccount)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.OutcomingOrders)
            .WithOne(e => e.IssuerAccount)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(e => e.AccountCode)
            .HasMaxLength(AccountValidationConstraints.CodeLengthFixed)
            .IsFixedLength();
    }
}