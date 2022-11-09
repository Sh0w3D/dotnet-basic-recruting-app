using FluentValidation;
using static MatchDataManager.Domain.Common.Constants.ErrorMessages;

namespace MatchDataManager.Application.Locations.Command.DeleteLocation;

public class DeleteLocationCommandValidator : AbstractValidator<DeleteLocationCommand>
{
    public DeleteLocationCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(Validation.IdEmpty);
    }
}