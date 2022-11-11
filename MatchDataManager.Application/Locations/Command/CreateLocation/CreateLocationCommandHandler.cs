using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Locations.Command.CreateLocation;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Location>
{
    private readonly ILocationCommandRepository _locationCommandRepository;

    public CreateLocationCommandHandler(ILocationCommandRepository locationCommandRepository)
    {
        _locationCommandRepository = locationCommandRepository;
    }

    public async Task<Location> Handle(
        CreateLocationCommand command,
        CancellationToken cancellationToken)
    {
        var location = new Location(command.Name, command.City);

        await _locationCommandRepository.CreateAsync(location, cancellationToken);

        return location;
    }
}