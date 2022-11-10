using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Locations.Query.GetLocation;

public record GetLocationByIdQuery(
    Guid Id)
    : IRequest<Location?>;