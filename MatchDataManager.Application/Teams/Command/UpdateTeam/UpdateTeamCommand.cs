using MediatR;

namespace MatchDataManager.Application.Teams.Command.UpdateTeam;

public record UpdateTeamCommand(
    Guid Id,
    string Name,
    string? CoachName
) : IRequest;