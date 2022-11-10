using FluentValidation;
using static MatchDataManager.Domain.Common.Constants.ErrorMessages;

namespace MatchDataManager.Application.Teams.Command.DeleteTeam;

public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
{
    public DeleteTeamCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(Validation.IdEmpty);
    }
}