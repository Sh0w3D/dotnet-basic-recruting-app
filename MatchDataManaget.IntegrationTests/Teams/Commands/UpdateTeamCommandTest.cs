using MatchDataManager.Application.Common.Exceptions.Shared;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Application.Teams.Command.UpdateTeam;
using MatchDataManaget.IntegrationTests.Teams.Mocks;
using MediatR;
using Xunit;

namespace MatchDataManaget.IntegrationTests.Teams.Commands;

public class UpdateTeamCommandTest
{
    private readonly ITeamCommandRepository _mockTeamCommandRepository;
    private readonly ITeamCommandRepository _mockTeamCommandRepositoryException;
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

    public UpdateTeamCommandTest()
    {
        _mockTeamCommandRepository = MockTeamCommandRepository.GetTeamRepository();
        _mockTeamCommandRepositoryException = MockTeamCommandRepository.GetTeamRepositoryException();
    }

    [Fact]
    public async Task UpdateTeamCommandShouldSucceed()
    {
        var command = new UpdateTeamCommand(Guid.NewGuid(), "LKS Buk Rudy", "Kamil");

        var handler = new UpdateTeamCommandHandler(
            _mockTeamCommandRepository);

        var result = await handler.Handle(command, _cancellationToken);

        Assert.Equal(Unit.Value, result);
    }

    [Fact]
    public async Task UpdateTeamCommandShouldSucceedCoachNameNull()
    {
        var command = new UpdateTeamCommand(Guid.NewGuid(), "LKS Buk Rudy", null);

        var handler = new UpdateTeamCommandHandler(
            _mockTeamCommandRepository);

        var result = await handler.Handle(command, _cancellationToken);

        Assert.Equal(Unit.Value, result);
    }

    [Fact]
    public async Task UpdateTeamCommandThrowsSaveToDatabaseException()
    {
        var command = new UpdateTeamCommand(Guid.NewGuid(), "LKS Buk Rudy", "Kamil");

        var handler = new UpdateTeamCommandHandler(_mockTeamCommandRepositoryException);

        var exception = await Record.ExceptionAsync(() =>
            handler.Handle(command, _cancellationToken));

        Assert.IsType<SaveToDatabaseException>(exception);
    }
}