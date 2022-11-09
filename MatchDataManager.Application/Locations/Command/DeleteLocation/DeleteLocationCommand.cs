using MediatR;

namespace MatchDataManager.Application.Locations.Command.DeleteLocation;

public record DeleteLocationCommand (
    Guid Id
) : IRequest;