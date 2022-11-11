using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Application.Teams.Command.UpdateTeam;
using MatchDataManager.Domain.Common.Constants;
using MatchDataManager.UnitTests.Team.Mocks;
using MatchDataManager.UnitTests.TestData;
using Xunit;

namespace MatchDataManager.UnitTests.Team.UpdateTeam;

public class UpdateTeamCommandTest
{
    private readonly ITeamQueryRepository _teamRepositoryUniqueNameFalse;
    private readonly ITeamQueryRepository _teamRepositoryUniqueNameTrue;

    public UpdateTeamCommandTest()
    {
        _teamRepositoryUniqueNameFalse = MockTeamQueryRepository.GetTeamUniqueName(false);
        _teamRepositoryUniqueNameTrue = MockTeamQueryRepository.GetTeamUniqueName(true);
    }

    [Fact]
    public void UpdateTeamCommandValidatorShouldThrowUniqueNameIsRequired()
    {
        var team = new UpdateTeamCommand(Guid.NewGuid(), "SRC", "Rudy");

        var validator = new UpdateTeamCommandValidator(_teamRepositoryUniqueNameFalse);

        var result = validator.ValidateAsync(team);

        result.Result.Errors.ForEach(error =>
            Assert.Equal(ErrorMessages.Validation.TeamNameUnique, error.ErrorMessage));
    }

    [Theory]
    [MemberData(nameof(UpdateTeamTestData))]
    public void UpdateTeamCommandValidatorShouldThrowPropertyIsRequired(
        UpdateTeamCommand team,
        string expected)
    {
        var validator = new UpdateTeamCommandValidator(_teamRepositoryUniqueNameTrue);

        var result = validator.ValidateAsync(team);
        result.Result.Errors.ForEach(error =>
            Assert.Equal(expected, error.ErrorMessage));
    }

    [Fact]
    public void UpdateTeamCommandValidatorShouldThrowTooLongName()
    {
        var team = new UpdateTeamCommand(Guid.NewGuid(),
            SharedTestData.NameTooLong,
            "Rybnik");

        var validator = new UpdateTeamCommandValidator(_teamRepositoryUniqueNameTrue);

        var result = validator.ValidateAsync(team);

        result.Result.Errors.ForEach(error =>
            Assert.Equal(ErrorMessages.Validation.TeamNameLength, error.ErrorMessage));
    }

    [Fact]
    public void UpdateTeamCommandValidatorShouldThrowTooLongCoachName()
    {
        var team = new UpdateTeamCommand(Guid.NewGuid(),
            "SRC",
            SharedTestData.CTooLong);

        var validator = new UpdateTeamCommandValidator(_teamRepositoryUniqueNameTrue);

        var result = validator.ValidateAsync(team);

        result.Result.Errors.ForEach(error =>
            Assert.Equal(ErrorMessages.Validation.TeamCoachNameLength, error.ErrorMessage));
    }


    private static IEnumerable<object[]> UpdateTeamTestData()
    {
        // Team name data
        yield return new object[]
        {
            new UpdateTeamCommand(Guid.NewGuid(), " ", "Rudy"),
            ErrorMessages.Validation.TeamNameRequired
        };
        yield return new object[]
        {
            new UpdateTeamCommand(Guid.NewGuid(), "", "Katowice"),
            ErrorMessages.Validation.TeamNameRequired
        };
        yield return new object[]
        {
            new UpdateTeamCommand(Guid.NewGuid(), null!, "Gliwice"),
            ErrorMessages.Validation.TeamNameRequired
        };
    }
}