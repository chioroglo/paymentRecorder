﻿using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Common.Validation.ValidationConstraints.ApplicationUserValidationConstraints;

namespace Data.EntityConfiguration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable(nameof(ApplicationUser));

        builder.Property(e => e.Firstname)
            .HasMaxLength(FirstnameLastnameMaxLength)
            .IsRequired(false);

        builder.Property(e => e.Lastname)
            .HasMaxLength(FirstnameLastnameMaxLength)
            .IsRequired(false);
    }
}