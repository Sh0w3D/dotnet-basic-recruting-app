using MatchDataManager.Domain.Entities;

namespace MatchDataManager.Application.Common.Interfaces.Repositories.Query;

public interface ITeamQueryRepository
{
    Task<Team?> GetTeamAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<List<Team>> GetTeamsAsync(
        CancellationToken cancellationToken = default);

    Task<bool> UniqueNameAsync(
        string name,
        CancellationToken cancellationToken = default);

    Task<Team?> GetTeamByNameAsync(
        string name,
        CancellationToken cancellationToken = default);
}