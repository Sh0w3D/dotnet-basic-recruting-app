using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Teams.Query.GetTeams;

public record GetTeamsQuery : IRequest<List<Team>>;