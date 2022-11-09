using FluentValidation;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using static MatchDataManager.Domain.Common.Constants.ErrorMessages;

namespace MatchDataManager.Application.Locations.Command.CreateLocation;

public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    private readonly ILocationQueryRepository _locationQueryRepository;
    public CreateLocationCommandValidator(ILocationQueryRepository locationQueryRepository)
    {
        _locationQueryRepository = locationQueryRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(Validation.LocationNameRequired)
            .MaximumLength(255).WithMessage(Validation.LocationNameLength)
            .MustAsync(BeUnique).WithMessage(Validation.LocationUnique);

        RuleFor(p => p.City)
            .NotEmpty().WithMessage(Validation.LocationCityRequired)
            .MaximumLength(55).WithMessage(Validation.LocationCityLength);
    }

    private async Task<bool> BeUnique(string name, CancellationToken cancellationToken)
    {
        return await _locationQueryRepository.UniqueNameAsync(name, cancellationToken);
    }
}