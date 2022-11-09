using MatchDataManager.Domain.Entities;

namespace MatchDataManager.Application.Common.Interfaces.Repositories.Query;

public interface ILocationQueryRepository
{
    Task<Location?> GetLocationAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Location>> GetLocationsAsync(
        CancellationToken cancellationToken = default);
}