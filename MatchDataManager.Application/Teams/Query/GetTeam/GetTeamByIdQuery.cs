using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Teams.Query.GetTeam;

public record GetTeamByIdQuery(
    Guid Id
): IRequest<Team?>;