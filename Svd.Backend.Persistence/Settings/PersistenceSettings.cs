using System.ComponentModel.DataAnnotations;
using Svd.Backend.Application.Attributes;

namespace Svd.Backend.Persistence.Settings;

#pragma warning disable CS8618
public class PersistenceSettings
{
    [Required]
    [MapEnvironmentVariable("DB_CONNECTION_STRING")]
    public string DbConnectionString { get; set; }
}
#pragma warning restore CS8618
