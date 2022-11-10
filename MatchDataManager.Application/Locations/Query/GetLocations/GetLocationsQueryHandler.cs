using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Locations.Query.GetLocations;

public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, List<Location>>
{
    private readonly ILocationQueryRepository _locationQueryRepository;

    public GetLocationsQueryHandler(ILocationQueryRepository locationQueryRepository)
    {
        _locationQueryRepository = locationQueryRepository;
    }

    public async Task<List<Location>> Handle(
        GetLocationsQuery query,
        CancellationToken cancellationToken)
    {
        return await _locationQueryRepository.GetLocationsAsync(cancellationToken);
    }
}