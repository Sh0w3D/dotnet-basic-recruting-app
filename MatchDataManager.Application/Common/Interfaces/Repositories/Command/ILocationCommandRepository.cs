using MatchDataManager.Domain.Entities;

namespace MatchDataManager.Application.Common.Interfaces.Repositories.Command;

public interface ILocationCommandRepository
{
    Task CreateAsync(
        Location location,
        CancellationToken cancellationToken = default);

    Task DeleteLocationAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task UpdateLocationAsync(
        Location location,
        CancellationToken cancellationToken = default);
}