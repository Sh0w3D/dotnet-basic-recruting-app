using MediatR;

namespace MatchDataManager.Application.Teams.Command.Delete;

public record DeleteTeamCommand(
    Guid Id
) : IRequest;