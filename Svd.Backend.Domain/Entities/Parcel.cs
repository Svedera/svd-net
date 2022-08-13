using System.ComponentModel.DataAnnotations;
using Svd.Backend.Domain.Common;

namespace Svd.Backend.Domain.Entities;

#pragma warning disable CS8618
public class Parcel : AuditableEntity
{
    public Guid ParcelId { get; set; }
    [Required] public string ParcelNumber { get; set; }
    [Required] public string RecipientName { get; set; }

    public double Weight { get; set; }
    public double Price { get; set; }

    public CountryDetails Country { get; set; }
    public Guid BagId { get; set; }
}
#pragma warning restore CS8618
