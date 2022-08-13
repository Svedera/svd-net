using System.Collections.Immutable;
using Svd.Backend.Domain.Entities;
using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Persistence.Defaults;

public static class CountryDefaults
{
    public static readonly ImmutableArray<CountryDetails> Countries =
        ImmutableArray.Create(
            new[]
            {
                new CountryDetails()
                {
                    CountryId = new Guid("6deb7ba4-cba3-4b82-ab68-9456bfa8eaec"),
                    Name = "Estonia",
                    Country = Country.Estonia,
                    Code = "EE"
                },
                new CountryDetails()
                {
                    CountryId = new Guid("ccf08179-22e9-4239-b02b-cc9042dc517f"),
                    Name = "Latvia",
                    Country = Country.Latvia,
                    Code = "LV"
                },
                new CountryDetails()
                {
                    CountryId = new Guid("446f7076-0590-483c-9a9a-96c9e62a972a"),
                    Name = "Finland",
                    Country = Country.Finland,
                    Code = "FI"
                }
            });
}
