using MediatR;

namespace Svd.Backend.Application.Features.Bags.Queries.GetBagsList;

public class GetBagsListQuery : IRequest<List<BagViewModel>>
{
}
