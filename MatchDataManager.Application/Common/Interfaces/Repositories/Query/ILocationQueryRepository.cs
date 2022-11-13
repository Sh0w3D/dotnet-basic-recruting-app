using MatchDataManager.Domain.Entities;

namespace MatchDataManager.Application.Common.Interfaces.Repositories.Query;

public interface ILocationQueryRepository
{
    Task<Location?> GetLocationAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<List<Location>> GetLocationsAsync(
        CancellationToken cancellationToken = default);

    Task<bool> UniqueNameAsync(
        string name,
        CancellationToken cancellationToken = default);

    Task<Location?> GetLocationByNameAsync(
        string name,
        CancellationToken cancellationToken = default);
}