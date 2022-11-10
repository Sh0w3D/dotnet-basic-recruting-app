using System.Reflection.Metadata;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Teams.Command.UpdateTeam;

public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand>
{
    private readonly ITeamCommandRepository _teamCommandRepository;
    public UpdateTeamCommandHandler(ITeamCommandRepository teamCommandRepository)
    {
        _teamCommandRepository = teamCommandRepository;
    }

    public async Task<Unit> Handle(
        UpdateTeamCommand command,
        CancellationToken cancellationToken
    )
    {
        var newTeam = new Team
        {
            Id = command.Id,
            Name = command.Name,
            CoachName = command.CoachName
        };

        await _teamCommandRepository.UpdateTeamAsync(newTeam, cancellationToken);

        return Unit.Value;
    }
}