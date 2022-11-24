using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Common.ValidationConstraints.OrderValidationConstraints;


namespace Data.EntityConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order));

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Number)
            .IsUnique();

        builder.Property(e => e.Version)
            .IsConcurrencyToken()
            .HasDefaultValueSql(UtilSqlCommands.SqlServerNewGuidCommand);

        builder.Property(e => e.CurrencyCode);

        builder.Property(e => e.Destination)
            .HasMaxLength(DestinationMaxLength);

        builder.Property(e => e.ExecutionDate)
            .IsRequired(false);

        builder.HasOne(e => e.Transaction)
            .WithOne(e => e.Order)
            .HasForeignKey<Transaction>(e => e.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}