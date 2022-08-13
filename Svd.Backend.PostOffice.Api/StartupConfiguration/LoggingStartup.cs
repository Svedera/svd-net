using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Svd.Backend.PostOffice.Settings;

namespace Svd.Backend.PostOffice.StartupConfiguration;

/// <summary>
/// This class contains different helper methods for logging and application insights
/// </summary>
public static class LoggingStartup
{
    /// <summary>
    /// Configure simple logging to console
    /// </summary>
    public static void ConfigureBootstrapSerilog(
        LogEventLevel minimumLevel = LogEventLevel.Debug)
    {
        var configuration = new LoggerConfiguration()
            .MinimumLevel.Is(minimumLevel)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Console();

        Log.Logger = configuration
            .WriteTo.Console()
            .CreateBootstrapLogger();
    }

    public static void ConfigureSerilog(
        this WebApplicationBuilder builder,
        AppSettings settings)
    {
        if (settings is null)
            throw new ArgumentNullException(nameof(settings));

        builder.Host.UseSerilog((_, configuration) => configuration
            .MinimumLevel.Is(settings.Serilog.LogLevel)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Console()
            .WriteTo.MSSqlServer(
                settings.Persistence.DbConnectionString,
                settings.Serilog.SerilogSql));
    }
}
