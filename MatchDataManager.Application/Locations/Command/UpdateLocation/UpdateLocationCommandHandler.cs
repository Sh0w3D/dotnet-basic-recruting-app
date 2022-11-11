using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Locations.Command.UpdateLocation;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand>
{
    private readonly ILocationCommandRepository _locationCommandRepository;

    public UpdateLocationCommandHandler(
        ILocationCommandRepository locationCommandRepository)
    {
        _locationCommandRepository = locationCommandRepository;
    }

    public async Task<Unit> Handle(
        UpdateLocationCommand command,
        CancellationToken cancellationToken)
    {
        var newLocation = new Location
        {
            Id = command.Id,
            Name = command.Name,
            City = command.City
        };

        await _locationCommandRepository.UpdateLocationAsync(newLocation, cancellationToken);

        return await Task.FromResult(Unit.Value);
    }
}