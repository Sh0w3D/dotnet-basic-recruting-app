using MatchDataManager.Domain.Entities;

namespace MatchDataManager.Application.Common.Interfaces.Repositories.Command;

public interface ITeamCommandRepository
{
    Task CreateTeamAsync(
        Team team,
        CancellationToken cancellationToken = default);

    Task DeleteTeamAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task UpdateTeamAsync(
        Team team,
        CancellationToken cancellationToken = default);
}