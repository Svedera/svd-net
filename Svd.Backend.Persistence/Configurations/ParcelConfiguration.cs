using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Persistence.Configurations;

public class ParcelConfiguration :
    IEntityTypeConfiguration<Parcel>
{
    public void Configure(EntityTypeBuilder<Parcel> builder)
    {
        builder.HasIndex(parcel => parcel.ParcelNumber)
            .IsUnique();
        builder.Property(parcel => parcel.ParcelNumber)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnType("varchar(10)");
        builder.Property(parcel => parcel.RecipientName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");
    }
}
