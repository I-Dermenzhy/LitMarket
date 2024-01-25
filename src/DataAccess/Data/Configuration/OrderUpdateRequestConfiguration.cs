using Domain.Models.Orders;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configuration;
internal sealed class OrderUpdateRequestConfiguration : IEntityTypeConfiguration<OrderUpdateRequest>
{
    public void Configure(EntityTypeBuilder<OrderUpdateRequest> builder)
    {
        builder.OwnsOne(our => our.ShippingAddressUpdate, a =>
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
