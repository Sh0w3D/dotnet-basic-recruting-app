using MatchDataManager.Application.Common.Interfaces;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchDataManager.Infrastructure.Repositories.Queries;

public class LocationQueryRepository : ILocationQueryRepository
{
    private readonly IApplicationDbContext _context;

    public LocationQueryRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Location?> GetLocationAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Locations
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Location>> GetLocationsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Locations
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}