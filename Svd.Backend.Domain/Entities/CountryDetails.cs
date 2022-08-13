using System.ComponentModel.DataAnnotations;
using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Domain.Entities;

#pragma warning disable CS8618
public class CountryDetails
{
    public Guid CountryId { get; set; }
    public Country Country { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Code { get; set; }
}
#pragma warning restore CS8618
