using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Application.Locations.Command.CreateLocation;
using MatchDataManager.UnitTests.Location.Mocks;
using MatchDataManager.UnitTests.TestData;
using Xunit;
using static MatchDataManager.Domain.Common.Constants.ErrorMessages;

namespace MatchDataManager.UnitTests.Location.CreateLocation;

public class CreateLocationCommandTest
{
    private readonly ILocationQueryRepository _locationRepositoryUniqueNameFalse;
    private readonly ILocationQueryRepository _locationRepositoryUniqueNameTrue;

    public CreateLocationCommandTest()
    {
        _locationRepositoryUniqueNameFalse = MockLocationQueryRepository.GetLocationUniqueName(false);
        _locationRepositoryUniqueNameTrue = MockLocationQueryRepository.GetLocationUniqueName(true);
    }

    [Theory]
    [MemberData(nameof(CreateLocationTestData))]
    public void CreateLocationCommandValidatorShouldThrowPropertyIsRequired(
        CreateLocationCommand command, string expected)
    {
        var validator = new CreateLocationCommandValidator(_locationRepositoryUniqueNameTrue);

        var result = validator.ValidateAsync(command);

        result.Result.Errors.ForEach(error =>
            Assert.Equal(expected, error.ErrorMessage));
    }

    [Fact]
    public void CreateLocationCommandValidatorShouldThrowUniqueNameIsRequired()
    {
        var location = new CreateLocationCommand("SRC", "Rudy");

        var validator = new CreateLocationCommandValidator(_locationRepositoryUniqueNameFalse);

        var result = validator.ValidateAsync(location);

        result.Result.Errors.ForEach(error =>
            Assert.Equal(Validation.LocationUnique, error.ErrorMessage));
    }

    [Fact]
    public void CreateLocationCommandValidatorShouldThrowNameTooLong()
    {
        var location = new CreateLocationCommand(SharedTestData.NameTooLong, "Rybnik");

        var validator = new CreateLocationCommandValidator(_locationRepositoryUniqueNameTrue);

        var result = validator.ValidateAsync(location);

        result.Result.Errors.ForEach(error =>
            Assert.Equal(Validation.LocationNameLength, error.ErrorMessage));
    }

    [Fact]
    public void CreateLocationCommandValidatorShouldThrowCityTooLong()
    {
        var location = new CreateLocationCommand("SRC", SharedTestData.CTooLong);

        var validator = new CreateLocationCommandValidator(_locationRepositoryUniqueNameTrue);

        var result = validator.ValidateAsync(location);

        result.Result.Errors.ForEach(error =>
            Assert.Equal(Validation.LocationCityLength, error.ErrorMessage));
    }

    private static IEnumerable<object[]> CreateLocationTestData()
    {
        // Location name data
        yield return new object[]
        {
            new CreateLocationCommand(" ", "Rudy"),
            Validation.LocationNameRequired
        };
        yield return new object[]
        {
            new CreateLocationCommand("", "Katowice"),
            Validation.LocationNameRequired
        };
        yield return new object[]
        {
            new CreateLocationCommand(null!, "Gliwice"),
            Validation.LocationNameRequired
        };
        // Location city data
        yield return new object[]
        {
            new CreateLocationCommand("SRC", " "),
            Validation.LocationCityRequired
        };
        yield return new object[]
        {
            new CreateLocationCommand("SRB", ""),
            Validation.LocationCityRequired
        };
        yield return new object[]
        {
            new CreateLocationCommand("SR", null!),
            Validation.LocationCityRequired
        };
    }
}