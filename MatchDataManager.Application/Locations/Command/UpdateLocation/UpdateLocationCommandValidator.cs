﻿using FluentValidation;
using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Common.Constants;

namespace MatchDataManager.Application.Locations.Command.UpdateLocation;
public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{

    /*private readonly ILocationQueryRepository _locationQueryRepository;*/

    public UpdateLocationCommandValidator(
        /*ILocationQueryRepository locationQueryRepository*/)
    {
/*        _locationQueryRepository = locationQueryRepository;*/

/*        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ErrorMessages.Validation.IdEmpty);*/

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ErrorMessages.Validation.LocationNameRequired)
            .MaximumLength(255).WithMessage(ErrorMessages.Validation.LocationNameLength);
           /* .MustAsync(BeUniqueAsync).WithMessage(ErrorMessages.Validation.LocationUnique);*/

        RuleFor(x => x.City)
            .NotEmpty().WithMessage(ErrorMessages.Validation.LocationCityRequired)
            .MaximumLength(55).WithMessage(ErrorMessages.Validation.LocationCityLength);
    }
/*
    private async Task<bool> BeUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return await _locationQueryRepository.UniqueNameAsync(name, cancellationToken);
    }*/
}
