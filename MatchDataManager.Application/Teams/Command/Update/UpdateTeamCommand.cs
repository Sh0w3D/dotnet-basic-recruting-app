using MediatR;

namespace MatchDataManager.Application.Teams.Command.Update;

public record UpdateTeamCommand(
    Guid Id,
    string Name,
    string CoachName
) : IRequest;