using System.ComponentModel.DataAnnotations;
using Svd.Backend.Domain.Common;
using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Domain.Entities;

#pragma warning disable CS8618
public class Bag : AuditableEntity
{
    public Guid BagId { get; set; }

    [Required]
    public string BagNumber { get; set; }

    public BagType Type { get; set; }
    public double Weight { get; set; }
    public double Price { get; set; }
    public double ItemsCount { get; set; }

    public Guid ShipmentId { get; set; }
}
#pragma warning restore CS8618
