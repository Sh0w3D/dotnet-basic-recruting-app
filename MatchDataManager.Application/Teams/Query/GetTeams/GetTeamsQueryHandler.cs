using System.Reflection.Metadata;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Teams.Query.GetTeams;

public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, List<Team>>
{
    private readonly ITeamQueryRepository _teamQueryRepository;

    public GetTeamsQueryHandler(ITeamQueryRepository teamQueryRepository)
    {
        _teamQueryRepository = teamQueryRepository;
    }

    public async Task<List<Team>> Handle(
        GetTeamsQuery query,
        CancellationToken cancellationToken
    )
    {
        return await _teamQueryRepository.GetTeamsAsync(cancellationToken);
    }
}