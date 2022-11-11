using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Application.Locations.Command.UpdateLocation;
using MatchDataManager.Domain.Common.Constants;
using MatchDataManager.UnitTests.Location.Mocks;
using MatchDataManager.UnitTests.TestData;
using Xunit;

namespace MatchDataManager.UnitTests.Location.UpdateLocation;

public class UpdateLocationCommandTest
{
    private readonly ILocationQueryRepository _locationRepositoryUniqueNameFalse;
    private readonly ILocationQueryRepository _locationRepositoryUniqueNameTrue;

    public UpdateLocationCommandTest()
    {
        _locationRepositoryUniqueNameFalse = MockLocationQueryRepository.GetLocationUniqueName(false);
        _locationRepositoryUniqueNameTrue = MockLocationQueryRepository.GetLocationUniqueName(true);
    }

    [Fact]
    public void UpdateLocationCommandValidatorShouldThrowUniqueNameIsRequired()
    {
        var location = new UpdateLocationCommand(Guid.NewGuid(), "SRC", "Rudy");

        var validator = new UpdateLocationCommandValidator(_locationRepositoryUniqueNameFalse);

        var result = validator.ValidateAsync(location);

        result.Result.Errors.ForEach(error =>
            Assert.Equal(ErrorMessages.Validation.LocationUnique, error.ErrorMessage));
    }

    [Theory]
    [MemberData(nameof(UpdateLocationTestData))]
    public void UpdateLocationCommandValidatorShouldThrowPropertyIsRequired(
        UpdateLocationCommand location,
        string expected)
    {
        var validator = new UpdateLocationCommandValidator(_locationRepositoryUniqueNameTrue);

        var result = validator.ValidateAsync(location);
        result.Result.Errors.ForEach(error =>
            Assert.Equal(expected, error.ErrorMessage));
    }

    [Fact]
    public void UpdateLocationCommandValidatorShouldThrowTooLongName()
    {
        var location = new UpdateLocationCommand(Guid.NewGuid(),
            SharedTestData.NameTooLong,
            "Rybnik");

        var validator = new UpdateLocationCommandValidator(_locationRepositoryUniqueNameTrue);

        var result = validator.ValidateAsync(location);

        result.Result.Errors.ForEach(error =>
            Assert.Equal(ErrorMessages.Validation.LocationNameLength, error.ErrorMessage));
    }

    [Fact]
    public void UpdateLocationCommandValidatorShouldThrowTooLongCity()
    {
        var location = new UpdateLocationCommand(Guid.NewGuid(),
            "SRC",
            SharedTestData.CTooLong);

        var validator = new UpdateLocationCommandValidator(_locationRepositoryUniqueNameTrue);

        var result = validator.ValidateAsync(location);

        result.Result.Errors.ForEach(error =>
            Assert.Equal(ErrorMessages.Validation.LocationCityLength, error.ErrorMessage));
    }

    private static IEnumerable<object[]> UpdateLocationTestData()
    {
        // Location name data
        yield return new object[]
        {
            new UpdateLocationCommand(Guid.NewGuid(), " ", "Rudy"),
            ErrorMessages.Validation.LocationNameRequired
        };
        yield return new object[]
        {
            new UpdateLocationCommand(Guid.NewGuid(), "", "Katowice"),
            ErrorMessages.Validation.LocationNameRequired
        };
        yield return new object[]
        {
            new UpdateLocationCommand(Guid.NewGuid(), null!, "Gliwice"),
            ErrorMessages.Validation.LocationNameRequired
        };
        // Location city data
        yield return new object[]
        {
            new UpdateLocationCommand(Guid.NewGuid(), "SRC", " "),
            ErrorMessages.Validation.LocationCityRequired
        };
        yield return new object[]
        {
            new UpdateLocationCommand(Guid.NewGuid(), "SRB", ""),
            ErrorMessages.Validation.LocationCityRequired
        };
        yield return new object[]
        {
            new UpdateLocationCommand(Guid.NewGuid(), "SR", null!),
            ErrorMessages.Validation.LocationCityRequired
        };
    }
}