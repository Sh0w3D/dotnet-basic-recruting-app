using FluentValidation;
using static MatchDataManager.Domain.Common.Constants.ErrorMessages;

namespace MatchDataManager.Application.Teams.Query.GetTeam;

public class GetTeamByIdQueryValidator : AbstractValidator<GetTeamByIdQuery>
{
    public GetTeamByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(Validation.IdEmpty);
    }
}