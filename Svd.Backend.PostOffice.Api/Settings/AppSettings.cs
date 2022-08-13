using Svd.Backend.Application.Configurations;
using Svd.Backend.Persistence.Settings;

namespace Svd.Backend.PostOffice.Settings;

#pragma warning disable CS8618
public class AppSettings : ValidatedSettings
{
    public LoggingSettings Serilog { get; set; }
    public SwaggerSettings Swagger { get; set; }
    public ShipmentSettings Shipment { get; set; }
    public PersistenceSettings Persistence { get; set; }
}
#pragma warning restore CS8618
