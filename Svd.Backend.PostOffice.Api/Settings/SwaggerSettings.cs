using System.ComponentModel.DataAnnotations;

namespace Svd.Backend.PostOffice.Settings;

#pragma warning disable CS8618
public class SwaggerSettings
{
    [Required] public string Version { get; set; }
    [Required] public string Title { get; set; }
}
#pragma warning restore CS8618
