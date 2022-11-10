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
            .MaximumLength(255).WithMessage(ErrorMessages.Validation.TeamNameLength)
            .MustAsync(BeUniqueAsync).WithMessage(ErrorMessages.Validation.TeamNameUnique);

        RuleFor(p => p.CoachName)
            .MaximumLength(55).WithMessage(ErrorMessages.Validation.TeamCoachNameLength);
    }

    private async Task<bool> BeUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return await _teamQueryRepository.UniqueNameAsync(name, cancellationToken);
    }
}