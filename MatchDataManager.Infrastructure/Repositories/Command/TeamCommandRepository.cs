using MatchDataManager.Application.Common.Exceptions.Shared;
using MatchDataManager.Application.Common.Interfaces;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Entities;

namespace MatchDataManager.Infrastructure.Repositories.Command.Query;

public class TeamCommandRepository : ITeamCommandRepository
{
    private readonly IApplicationDbContext _context;
    private readonly ITeamQueryRepository _teamQueryRepository;

    public TeamCommandRepository(
        IApplicationDbContext context,
        ITeamQueryRepository teamQueryRepository)
    {
        _context = context;
        _teamQueryRepository = teamQueryRepository;
    }

    public async Task CreateTeamAsync(
        Team team,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Teams.AddAsync(team, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SaveToDatabaseException(nameof(CreateTeamAsync), ex);
        }
    }

    public async Task DeleteTeamAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var toDelete = await _teamQueryRepository.GetTeamAsync(id, cancellationToken);
            if (toDelete is null)
                throw new NotFoundException(nameof(DeleteTeamAsync));

            _context.Teams.Remove(toDelete);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new SaveToDatabaseException(nameof(DeleteTeamAsync), ex);
        }
    }

    public async Task UpdateTeamAsync(
        Team team,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var teamEntity = await _teamQueryRepository.GetTeamAsync(team.Id, cancellationToken);
            if (teamEntity is null)
                throw new NotFoundException(nameof(UpdateTeamAsync));

            teamEntity.Name = team.Name;
            teamEntity.CoachName = team.CoachName;

            _context.Teams.Update(teamEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new SaveToDatabaseException(nameof(UpdateTeamAsync), ex);
        }
    }
}