using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Persistence.Configurations;

public class AirportConfigurationShipmentConfiguration :
    IEntityTypeConfiguration<AirportDetails>
{
    public void Configure(EntityTypeBuilder<AirportDetails> builder)
    {
        builder.HasKey(airport => airport.AirportId);
        builder.Property(shipment => shipment.Code)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnType("varchar(3)");
    }
}
