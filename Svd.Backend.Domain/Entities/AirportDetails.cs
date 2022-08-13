using System.ComponentModel.DataAnnotations;
using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Domain.Entities;

#pragma warning disable CS8618
public class AirportDetails
{
    public Guid AirportId { get; set; }
    public Airport Airport { get; set; }
    [Required] public string Code { get; set; }
}
#pragma warning restore CS8618
