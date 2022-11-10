using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Teams.Command.CreateTeam;

public record CreateTeamCommand(
    string Name,
    string? CoachName
): IRequest<Team>;