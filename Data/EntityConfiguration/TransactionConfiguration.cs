using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfiguration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(nameof(Transaction));

        builder.Property(e => e.Version)
            .IsConcurrencyToken()
            .HasDefaultValueSql(UtilSqlCommands.SqlServerNewGuidCommand);

        builder.Property(e => e.Version).IsConcurrencyToken();
    }
}