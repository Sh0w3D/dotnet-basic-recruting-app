using FluentValidation;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using static MatchDataManager.Domain.Common.Constants.ErrorMessages;

namespace MatchDataManager.Application.Teams.Command.Create;

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    private readonly ITeamQueryRepository _teamQueryRepository;

    public CreateTeamCommandValidator(ITeamQueryRepository teamQueryRepository)
    {
        _teamQueryRepository = teamQueryRepository;

        RuleFor(p => p.CoachName)
            .MaximumLength(55).WithMessage(Validation.TeamCoachNameLength);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(Validation.TeamNameRequired)
            .MaximumLength(255).WithMessage(Validation.TeamNameLength)
            .MustAsync(BeUniqueAsync).WithMessage(Validation.TeamNameUnique);
    }

    private async Task<bool> BeUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return await _teamQueryRepository.UniqueNameAsync(name, cancellationToken);
    }
}