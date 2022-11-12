using MatchDataManager.Application.Common.Exceptions.Shared;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Application.Teams.Command.CreateTeam;
using MatchDataManaget.IntegrationTests.Teams.Mocks;
using Xunit;

namespace MatchDataManaget.IntegrationTests.Teams.Commands;

public class CreateTeamCommandTest
{
    private readonly ITeamCommandRepository _mockTeamCommandRepository;
    private readonly ITeamCommandRepository _mockTeamCommandRepositoryException;
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

    public CreateTeamCommandTest()
    {
        _mockTeamCommandRepository = MockTeamCommandRepository.GetTeamRepository();
        _mockTeamCommandRepositoryException = MockTeamCommandRepository.GetTeamRepositoryException();
    }

    [Fact]
    public async Task CreatTeamCommandShouldSucceed()
    {
        var command = new CreateTeamCommand("LKS Buk Rudy", "Kamil");

        var handler = new CreateTeamCommandHandler(_mockTeamCommandRepository);

        var result = await handler.Handle(command, _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.CoachName, result.CoachName);
    }

    [Fact]
    public async Task CreatTeamCommandShouldSucceedWithCoachNameNull()
    {
        var command = new CreateTeamCommand("LKS Buk Rudy", null);

        var handler = new CreateTeamCommandHandler(_mockTeamCommandRepository);

        var result = await handler.Handle(command, _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.CoachName, result.CoachName);
    }

    [Fact]
    public async Task CreateTeamCommandShouldThrow()
    {
        var command = new CreateTeamCommand("LKS Buk Rudy", "Kamil");

        var handler = new CreateTeamCommandHandler(_mockTeamCommandRepositoryException);

        var exception = await Record.ExceptionAsync(() =>
            handler.Handle(command, _cancellationToken));

        Assert.IsType<SaveToDatabaseException>(exception);
    }
}