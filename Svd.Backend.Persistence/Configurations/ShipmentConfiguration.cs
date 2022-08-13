using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Persistence.Configurations;

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.HasIndex(shipment => shipment.ShipmentNumber)
            .IsUnique();
        builder.Property(shipment => shipment.ShipmentNumber)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnType("varchar(10)");

        builder.Property(shipment => shipment.FlightNumber)
            .IsRequired()
            .HasMaxLength(6)
            .HasColumnType("varchar(6)");
    }
}
