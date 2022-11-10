using MediatR;

namespace MatchDataManager.Application.Locations.Command.UpdateLocation;

public record UpdateLocationCommand(
    Guid Id,
    string Name,
    string City) : IRequest;