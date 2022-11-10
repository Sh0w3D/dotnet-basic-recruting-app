using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Locations.Query.GetLocation;

public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, Location?>
{
    private readonly ILocationQueryRepository _locationQueryRepository;

    public GetLocationByIdQueryHandler(ILocationQueryRepository locationQueryRepository)
    {
        _locationQueryRepository = locationQueryRepository;
    }

    public async Task<Location?> Handle(
        GetLocationByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _locationQueryRepository
            .GetLocationAsync(query.Id, cancellationToken);
    }
}