using AutoMapper;
using Svd.Backend.Application.Features.Airports;
using Svd.Backend.Application.Features.Bags;
using Svd.Backend.Application.Features.Bags.Commands.CreateBag;
using Svd.Backend.Application.Features.Bags.Commands.CreateLetterBag;
using Svd.Backend.Application.Features.Countries;
using Svd.Backend.Application.Features.Parcels;
using Svd.Backend.Application.Features.Parcels.Commands.CreateParcel;
using Svd.Backend.Application.Features.Shipments;
using Svd.Backend.Application.Features.Shipments.Commands.CreateShipment;
using Svd.Backend.Domain.Entities;

namespace Svd.Backend.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AirportDetails, AirportViewModel>().ReverseMap();
        CreateMap<CountryDetails, CountryViewModel>().ReverseMap();

        CreateMap<CreateShipmentCommand, Shipment>()
            .ForMember(dest => dest.Airport,
                opt => opt.MapFrom(src => new AirportDetails()
                {
                    AirportId = src.AirportId
                }));
        CreateMap<Shipment, ShipmentViewModel>()
            .ForMember(dest => dest.BagCount,
                opt => opt.MapFrom(src => MapBagCount(src.Bags)));

        CreateMap<CreateParcelBagCommand, Bag>();
        CreateMap<CreateLetterBagCommand, Bag>();
        CreateMap<Bag, BagViewModel>();

        CreateMap<CreateParcelCommand, Parcel>().ReverseMap();
        CreateMap<Parcel, ParcelViewModel>();
    }

    private static int MapBagCount(List<Bag>? bags)
    {
        var count = bags?.Count() ?? 0;
        return count;
    }
}
