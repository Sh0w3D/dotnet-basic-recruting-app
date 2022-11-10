using MatchDataManager.Application.Common.Exceptions.Shared;
using MatchDataManager.Application.Common.Interfaces;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Entities;

namespace MatchDataManager.Infrastructure.Repositories.Command;

public class LocationCommandRepository : ILocationCommandRepository
{
    private readonly IApplicationDbContext _context;
    private readonly ILocationQueryRepository _locationQueryRepository;

    public LocationCommandRepository(
        IApplicationDbContext context,
        ILocationQueryRepository locationQueryRepository)
    {
        _context = context;
        _locationQueryRepository = locationQueryRepository;
    }

    public async Task CreateAsync(
        Location location,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Locations.AddAsync(location, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SaveToDatabaseException(nameof(CreateAsync), ex);
        }
    }

    public async Task DeleteLocationAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var toRemove = await _locationQueryRepository
                .GetLocationAsync(id, cancellationToken);
            if (toRemove is null)
                throw new NotFoundException(nameof(DeleteLocationAsync));

            _context.Locations.Remove(toRemove);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (NotFoundException)
        {
            throw new NotFoundException(nameof(DeleteLocationAsync));
        }
        catch (Exception ex)
        {
            throw new SaveToDatabaseException(nameof(DeleteLocationAsync), ex);
        }
    }

    public async Task UpdateLocationAsync(
        Location location,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var locationEntity = await _locationQueryRepository
                .GetLocationAsync(location.Id, cancellationToken);

            if (locationEntity is null)
                throw new NotFoundException(nameof(UpdateLocationAsync));

            locationEntity.Name = location.Name;
            locationEntity.City = location.City;

            _context.Locations.Update(locationEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (NotFoundException)
        {
            throw new NotFoundException(nameof(UpdateLocationAsync));
        }
        catch (Exception ex)
        {
            throw new SaveToDatabaseException(nameof(UpdateLocationAsync), ex);
        }
    }
}