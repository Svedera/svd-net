using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Svd.Backend.Application.Features.Parcels.Queries.GetParcel;

public class GetParcelQuery : IRequest<ParcelViewModel>
{
    [Required]
    public Guid ParcelId { get; set; }

    public GetParcelQuery(Guid parcelId)
    {
        ParcelId = parcelId;
    }
}
