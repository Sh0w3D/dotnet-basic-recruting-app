using MatchDataManager.Application.Common.Interfaces;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchDataManager.Infrastructure.Repositories.Queries;

public class TeamQueryRepository : ITeamQueryRepository
{
    private readonly IApplicationDbContext _context;

    public TeamQueryRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Team?> GetTeamAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Teams
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Team>> GetTeamsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Teams
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}