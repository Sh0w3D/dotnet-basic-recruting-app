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

    public async Task<List<Location>> GetLocationsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Locations
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

// do not use StringComparison, it brakes query!
// using pragma warning disable to disable unnecessary problems
#pragma warning disable RCS1155
    public async Task<bool> UniqueNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        var locationEntity = await _context.Locations
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                    x.Name.Trim().ToLower() == name.Trim().ToLower(),
                cancellationToken);

        return locationEntity is null;
    }

    public async Task<Location?> GetLocationByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        var locationEntity = await _context.Locations
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                    x.Name.Trim().ToLower() == name.Trim().ToLower(),
                cancellationToken);

        return locationEntity;
    }
#pragma warning restore RCS1155
}