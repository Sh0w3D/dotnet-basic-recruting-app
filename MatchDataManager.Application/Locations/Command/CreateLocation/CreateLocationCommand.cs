using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Locations.Command.CreateLocation;

public record CreateLocationCommand(
    string Name,
    string City) : IRequest<Location>;