using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Svd.Backend.Application.Contracts.Persistence;
using Svd.Backend.Persistence.Repositories;
using Svd.Backend.Persistence.Settings;

namespace Svd.Backend.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        PersistenceSettings settings)
    {
        services.AddDbContext<SvdDbContext>(
            options => options.UseSqlServer(settings.DbConnectionString));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IShipmentRepository, ShipmentRepository>();
        services.AddScoped<IBagRepository, BagRepository>();
        services.AddScoped<IParcelRepository, ParcelRepository>();

        return services;
    }
}
