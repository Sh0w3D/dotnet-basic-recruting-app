using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Teams.Query.GetTeam;

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, Team?>
{
    private readonly ITeamQueryRepository _teamQueryRepository;

    public GetTeamByIdQueryHandler(ITeamQueryRepository teamQueryRepository)
    {
        _teamQueryRepository = teamQueryRepository;
    }

    public async Task<Team?> Handle(
        GetTeamByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        return await _teamQueryRepository.GetTeamAsync(query.Id, cancellationToken);
    }
}