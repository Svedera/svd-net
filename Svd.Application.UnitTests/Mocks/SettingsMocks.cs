using Svd.Backend.Application.Configurations;

namespace Svd.Application.UnitTests.Mocks;

public static class SettingsMocks
{
    public static ShipmentSettings GetShipmentSettingsMock() =>
        new ShipmentSettings()
        {
            ShipmentNumberFormat = "^[A-Za-z0-9]{3}-[A-Za-z0-9]{6}$",
            FlightNumberFormat = "^[a-zA-Z]{2}[0-9]{4}$",
            BagNumberFormat = "^[A-Za-z0-9]{15}$",
            ParcelNumberFormat = "^[a-zA-Z]{2}[0-9]{6}[a-zA-Z]{2}$"
        };
}
