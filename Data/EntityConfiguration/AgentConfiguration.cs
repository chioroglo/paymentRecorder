using Common.Validation.ValidationConstraints;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfiguration;

public class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.ToTable(nameof(Agent));

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.FiscalCode)
            .IsUnique();

        builder.Property(e => e.FiscalCode)
            .HasMaxLength(AccountValidationConstraints.CodeLengthFixed)
            .IsFixedLength();

        builder.Property(e => e.Version)
            .IsConcurrencyToken()
            .HasDefaultValueSql(UtilSqlCommands.SqlServerNewGuidCommand);

        builder.Property(e => e.Name)
            .HasMaxLength(CommonValidationConstraints.NameMaxLength);

        builder.HasMany(e => e.Accounts)
            .WithOne(e => e.Agent)
            .OnDelete(DeleteBehavior.NoAction);

    }
}