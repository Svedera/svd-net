using System.Collections.Immutable;
using Svd.Backend.Domain.Entities;
using Svd.Backend.Domain.Enums;

namespace Svd.Backend.Persistence.Defaults;

public static class AirportDefaults
{
    public static readonly ImmutableArray<AirportDetails> Airports =
        ImmutableArray.Create(
            new[]
            {
                new AirportDetails()
                {
                    AirportId = new Guid("13904aa6-9f47-4579-9c1b-fc5a0a1f3961"),
                    Airport = Airport.Tll,
                    Code = "TLL"
                },
                new AirportDetails()
                {
                    AirportId = new Guid("0f9d2ea1-fd26-404a-9cdc-72eb09a9f191"),
                    Airport = Airport.Rix,
                    Code = "RIX"
                },
                new AirportDetails()
                {
                    AirportId = new Guid("4641bcfd-a227-4227-ad4d-ebdd9c65d185"),
                    Airport = Airport.Hel,
                    Code = "HEL"
                }
            });
}
