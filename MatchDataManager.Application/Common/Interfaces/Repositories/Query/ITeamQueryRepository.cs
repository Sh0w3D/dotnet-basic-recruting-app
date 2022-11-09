using MatchDataManager.Domain.Entities;

namespace MatchDataManager.Application.Common.Interfaces.Repositories.Query;

public interface ITeamQueryRepository
{
    Task<Team?> GetTeamAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Team>> GetTeamsAsync(
        CancellationToken cancellationToken = default);
}