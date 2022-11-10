using FluentValidation;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Common.Constants;

namespace MatchDataManager.Application.Locations.Command.UpdateLocation;
public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{

    private readonly ILocationQueryRepository _locationQueryRepository;
    public UpdateLocationCommandValidator(ILocationQueryRepository locationQueryRepository)
    {
        _locationQueryRepository = locationQueryRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ErrorMessages.Validation.LocationNameRequired)
            .MaximumLength(255).WithMessage(ErrorMessages.Validation.LocationNameLength)
            .MustAsync(BeUnique).WithMessage(ErrorMessages.Validation.LocationUnique);

        RuleFor(p => p.City)
            .NotEmpty().WithMessage(ErrorMessages.Validation.LocationCityRequired)
            .MaximumLength(55).WithMessage(ErrorMessages.Validation.LocationCityLength);
    }

    private async Task<bool> BeUnique(string name, CancellationToken cancellationToken)
    {
        return await _locationQueryRepository.UniqueNameAsync(name, cancellationToken);
    }
}
