using FluentValidation;
using static MatchDataManager.Domain.Common.Constants.ErrorMessages;

namespace MatchDataManager.Application.Locations.Query.GetLocation;

public class GetLocationByIdQueryValidator : AbstractValidator<GetLocationByIdQuery>
{
    public GetLocationByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(Validation.IdEmpty);
    }
}