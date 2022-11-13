using System.Data;
using FluentValidation;
using FluentValidation.Results;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Common.Constants;

namespace MatchDataManager.Application.Locations.Command.UpdateLocation;
public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
    private readonly ILocationQueryRepository _locationQueryRepository;
    public UpdateLocationCommandValidator(
        ILocationQueryRepository locationQueryRepository)
    {
        _locationQueryRepository = locationQueryRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ErrorMessages.Validation.LocationNameRequired)
            .MaximumLength(255).WithMessage(ErrorMessages.Validation.LocationNameLength);

        RuleFor(p => p.Name)
            .MustAsync(BeUnique)
            .WhenAsync(async (command, cancellationToken) =>
                await BeUniqueNameWithId(command.Id, command.Name, cancellationToken))
            .WithMessage(ErrorMessages.Validation.LocationUnique);

        RuleFor(p => p.City)
            .NotEmpty().WithMessage(ErrorMessages.Validation.LocationCityRequired)
            .MaximumLength(55).WithMessage(ErrorMessages.Validation.LocationCityLength);
    }

    private async Task<bool> BeUnique(string name, CancellationToken cancellationToken)
    {
        return await _locationQueryRepository.UniqueNameAsync(name, cancellationToken);
    }

    /// <summary>
    /// If user sending update command knows the specific id of team with specific name
    /// than user can change team properties, but if user tries to change name of
    /// specific team to other team name, than throw validation error
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> bool </returns>
    private async Task<bool> BeUniqueNameWithId(Guid id, string name, CancellationToken cancellationToken)
    {
        var location = await _locationQueryRepository.GetLocationByNameAsync(name, cancellationToken);
        if (location is null)
            return true;

        return location.Id != id;
    }
}