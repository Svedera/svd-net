using System.ComponentModel.DataAnnotations;
using Svd.Backend.Domain.Common;

namespace Svd.Backend.Domain.Entities;

#pragma warning disable CS8618
public class Shipment : AuditableEntity
{
    public Guid ShipmentId { get; set; }
    [Required] public string ShipmentNumber { get; set; }
    [Required] public string FlightNumber { get; set; }

    public DateTime FlightDate { get; set; }
    public bool Finalized { get; set; }

    public AirportDetails Airport { get; set; }
    public List<Bag>? Bags { get; set; }
}
#pragma warning restore CS8618
