using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Locations.Query.GetLocations;

public record GetLocationsQuery : IRequest<List<Location>>;