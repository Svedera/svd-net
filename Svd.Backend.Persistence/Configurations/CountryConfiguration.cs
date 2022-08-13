using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Persistence.Configurations;

public class CountryConfiguration :
    IEntityTypeConfiguration<CountryDetails>
{
    public void Configure(EntityTypeBuilder<CountryDetails> builder)
    {
        builder.HasKey(country => country.CountryId);
        builder.Property(shipment => shipment.Name)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnType("varchar(60)");
        builder.Property(shipment => shipment.Code)
            .IsRequired()
            .HasMaxLength(2)
            .HasColumnType("varchar(2)");
    }
}
