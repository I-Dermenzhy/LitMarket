using Domain.Models.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configuration;

internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.OwnsOne(u => u.FullAddress, a =>
        {
            a.Property(p => p.Country).HasMaxLength(60);
            a.Property(p => p.City).HasMaxLength(60);
            a.Property(p => p.StreetAddress).HasMaxLength(60);
            a.Property(p => p.PostalCode).HasMaxLength(6)
                .HasAnnotation("RegularExpression", @"^\d{6}$")
                .HasAnnotation("ErrorMessage", "Postal code must be exactly 6 digits.");
        });
    }
}
