using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MediatR;

namespace MatchDataManager.Application.Locations.Command.DeleteLocation;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand>
{
    private readonly ILocationCommandRepository _locationCommandRepository;

    public DeleteLocationCommandHandler(ILocationCommandRepository locationCommandRepository)
    {
        _locationCommandRepository = locationCommandRepository;
    }

    public async Task<Unit> Handle(
        DeleteLocationCommand command,
        CancellationToken cancellationToken
    )
    {
        await _locationCommandRepository.DeleteLocationAsync(command.Id, cancellationToken);

        return Unit.Value;
    }
}