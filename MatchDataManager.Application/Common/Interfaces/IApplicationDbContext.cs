using MatchDataManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchDataManager.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Location> Locations { get; }
    DbSet<Team> Teams { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}