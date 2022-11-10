using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MediatR;

namespace MatchDataManager.Application.Teams.Command.DeleteTeam;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand>
{
    private readonly ITeamCommandRepository _teamCommandRepository;

    public DeleteTeamCommandHandler(ITeamCommandRepository teamCommandRepository)
    {
        _teamCommandRepository = teamCommandRepository;
    }

    public async Task<Unit> Handle(
        DeleteTeamCommand command,
        CancellationToken cancellationToken
    )
    {
        await _teamCommandRepository.DeleteTeamAsync(command.Id, cancellationToken);

        return Unit.Value;
    }
}