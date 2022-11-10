using MediatR;

namespace MatchDataManager.Application.Teams.Command.DeleteTeam;

public record DeleteTeamCommand(
    Guid Id
) : IRequest;