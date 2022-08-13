using System.ComponentModel.DataAnnotations;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Svd.Backend.PostOffice.Settings;

#pragma warning disable CS8618
public class LoggingSettings
{
    public LogEventLevel LogLevel { get; set; } = LogEventLevel.Error;
    [Required]
    public MSSqlServerSinkOptions SerilogSql { get; set; }
}
#pragma warning restore CS8618
