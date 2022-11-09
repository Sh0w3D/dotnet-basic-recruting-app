using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Domain.Entities;
using MediatR;

namespace MatchDataManager.Application.Teams.Command.Create;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Team>
{
    private readonly ITeamCommandRepository _teamCommandRepository;
    public CreateTeamCommandHandler(ITeamCommandRepository teamCommandRepository)
    {
        _teamCommandRepository = teamCommandRepository;
    }

    public async Task<Team> Handle(
        CreateTeamCommand command,
        CancellationToken cancellationToken
    )
    {
        var newTeam = new Team
        {
            Name = command.Name,
            CoachName = command.CoachName
        };

        await _teamCommandRepository.CreateTeamAsync(newTeam, cancellationToken);

        return newTeam;
    }
}