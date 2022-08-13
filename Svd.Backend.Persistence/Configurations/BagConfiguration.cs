using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Persistence.Configurations;

public class BagConfiguration :
    IEntityTypeConfiguration<Bag>
{
    public void Configure(EntityTypeBuilder<Bag> builder)
    {
        builder.HasIndex(shipment => shipment.BagNumber)
            .IsUnique();
        builder.Property(shipment => shipment.BagNumber)
            .IsRequired()
            .HasMaxLength(15)
            .HasColumnType("varchar(15)");
    }
}
