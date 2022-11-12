using MatchDataManager.Application.Common.Exceptions.Shared;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Application.Teams.Command.DeleteTeam;
using MatchDataManaget.IntegrationTests.Teams.Mocks;
using MediatR;
using Xunit;

namespace MatchDataManaget.IntegrationTests.Teams.Commands;

public class DeleteTeamCommandTest
{
    private readonly ITeamCommandRepository _mockTeamCommandRepository;
    private readonly ITeamCommandRepository _mockTeamCommandRepositoryException;
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

    public DeleteTeamCommandTest()
    {
        _mockTeamCommandRepository = MockTeamCommandRepository.GetTeamRepository();
        _mockTeamCommandRepositoryException = MockTeamCommandRepository.GetTeamRepositoryException();
    }

    [Fact]
    public async Task DeleteTeamCommandShouldSucceed()
    {
        var command = new DeleteTeamCommand(Guid.NewGuid());

        var handler = new DeleteTeamCommandHandler(_mockTeamCommandRepository);

        var result = await handler.Handle(command, _cancellationToken);

        Assert.Equal(Unit.Value, result);
    }

    [Fact]
    public async Task DeleteTeamCommandShouldThrowSaveToDatabaseException()
    {
        var command = new DeleteTeamCommand(Guid.NewGuid());

        var handler = new DeleteTeamCommandHandler(_mockTeamCommandRepositoryException);

        var exception = await Record.ExceptionAsync(() =>
            handler.Handle(command, _cancellationToken));

        Assert.IsType<SaveToDatabaseException>(exception);
    }
}