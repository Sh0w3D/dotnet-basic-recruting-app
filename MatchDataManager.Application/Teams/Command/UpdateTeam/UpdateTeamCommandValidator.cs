using FluentValidation;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Common.Constants;

namespace MatchDataManager.Application.Teams.Command.UpdateTeam;

public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
{
    private readonly ITeamQueryRepository _teamQueryRepository;

    public UpdateTeamCommandValidator(ITeamQueryRepository teamQueryRepository)
    {
        _teamQueryRepository = teamQueryRepository;

        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(ErrorMessages.Validation.IdEmpty);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ErrorMessages.Validation.TeamNameRequired)
            .MaximumLength(255).WithMessage(ErrorMessages.Validation.TeamNameLength);

        RuleFor(p => p.Name)
            .MustAsync(BeUniqueAsync)
            .WhenAsync(async (command, cancellationToken) =>
                await BeUniqueNameWithId(command.Id, command.Name, cancellationToken))
            .WithMessage(ErrorMessages.Validation.TeamNameUnique);

        RuleFor(p => p.CoachName)
            .MaximumLength(55).WithMessage(ErrorMessages.Validation.TeamCoachNameLength);
    }

    private async Task<bool> BeUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return await _teamQueryRepository.UniqueNameAsync(name, cancellationToken);
    }

    /// <summary>
    /// If user sending update command knows the specific id of location with specific name
    /// than user can change location properties, but if user tries to change name of
    /// specific location to other location name, than throw validation error
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> bool </returns>
    private async Task<bool> BeUniqueNameWithId(Guid id, string name, CancellationToken cancellationToken)
    {
        var team = await _teamQueryRepository.GetTeamByNameAsync(name, cancellationToken);
        if (team is null)
            return true;

        return team.Id != id;
    }
}