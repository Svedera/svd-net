using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Application.Features.Countries;

public class CountryViewModel
{
    public CountryViewModel(Guid countryId, Country country, string name, string code)
    {
        CountryId = countryId;
        Country = country;
        Name = name;
        Code = code;
    }

    public Guid CountryId { get; set; }
    public Country Country { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}
