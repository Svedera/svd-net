using Microsoft.EntityFrameworkCore;
using Svd.Backend.Domain.Common;
using Svd.Backend.Domain.Entities;
using Svd.Backend.Persistence.Defaults;

namespace Svd.Backend.Persistence;

#pragma warning disable CS8618
public class SvdDbContext : DbContext
{
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Parcel> Parcels { get; set; }
    public DbSet<Bag> Bags { get; set; }

    public DbSet<CountryDetails> Countries { get; set; }
    public DbSet<AirportDetails> Airports { get; set; }


    public SvdDbContext(DbContextOptions<SvdDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SvdDbContext).Assembly);

        modelBuilder
            .Entity<CountryDetails>()
            .HasData(CountryDefaults.Countries);

        modelBuilder
            .Entity<AirportDetails>()
            .HasData(AirportDefaults.Airports);
    }

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
#pragma warning restore CS8618
