using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Application.Teams.Command.CreateTeam;
using MatchDataManager.Domain.Common.Constants;
using MatchDataManager.UnitTests.Team.Mocks;
using MatchDataManager.UnitTests.TestData;
using Xunit;

namespace MatchDataManager.UnitTests.Team.CreateTeam;

public class CreateTeamCommandTest
{
    private readonly ITeamQueryRepository _teamQueryRepositoryUniqueFalse;
    private readonly ITeamQueryRepository _teamQueryRepositoryUniqueTrue;

    public CreateTeamCommandTest()
    {
        _teamQueryRepositoryUniqueFalse = MockTeamQueryRepository.GetTeamUniqueName(false);
        _teamQueryRepositoryUniqueTrue = MockTeamQueryRepository.GetTeamUniqueName(true);
    }

    [Theory]
    [MemberData(nameof(CreateTeamTestData))]
    public void CreateTeamCommandValidatorShouldThrowPropertyIsRequired(
        CreateTeamCommand team,
        string expected)
    {
        var validator = new CreateTeamCommandValidator(_teamQueryRepositoryUniqueTrue);

        var result = validator.ValidateAsync(team);
        
        result.Result.Errors.ForEach(error =>
            Assert.Equal(expected, error.ErrorMessage));
    }

    [Fact]
    public void CreateTeamCommandValidatorShouldThrowUniqueName()
    {
        var team = new CreateTeamCommand("LKS Buk Rudy", "Kazimierz");

        var validator = new CreateTeamCommandValidator(_teamQueryRepositoryUniqueFalse);
        var result = validator.ValidateAsync(team);
        result.Result.Errors.ForEach(error =>
            Assert.Equal(ErrorMessages.Validation.TeamNameUnique, error.ErrorMessage));
    }

    [Fact]
    public void CreateTeamCommandValidatorShouldThrowNameTooLong()
    {
        var team = new CreateTeamCommand(SharedTestData.NameTooLong, "Karol");

        var validator = new CreateTeamCommandValidator(_teamQueryRepositoryUniqueTrue);

        var result = validator.ValidateAsync(team);
        
        result.Result.Errors.ForEach(error =>
            Assert.Equal(ErrorMessages.Validation.TeamNameLength, error.ErrorMessage));
    }

    [Fact]
    public void CreateTeamCommandValidatorShouldThrowCoachNameTooLong()
    {
        var team = new CreateTeamCommand("LKS Buk Rudy", SharedTestData.CTooLong);

        var validator = new CreateTeamCommandValidator(_teamQueryRepositoryUniqueTrue);

        var result = validator.ValidateAsync(team);
        result.Result.Errors.ForEach(error =>
            Assert.Equal(ErrorMessages.Validation.TeamCoachNameLength, error.ErrorMessage));
    }

    private static IEnumerable<object[]> CreateTeamTestData()
    {
        yield return new object[] { new CreateTeamCommand("", "Kamil"),
            ErrorMessages.Validation.TeamNameRequired };
        yield return new object[] { new CreateTeamCommand(" ", "Kamil"),
            ErrorMessages.Validation.TeamNameRequired };
        yield return new object[] { new CreateTeamCommand(null!, "Kamil"),
            ErrorMessages.Validation.TeamNameRequired };
    }
}