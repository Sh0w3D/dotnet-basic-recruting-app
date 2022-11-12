using MatchDataManager.Application.Common.Exceptions.Shared;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Application.Locations.Command.DeleteLocation;
using MatchDataManaget.IntegrationTests.Locations.Mocks;
using MediatR;
using Xunit;

namespace MatchDataManaget.IntegrationTests.Locations.Command;

public class DeleteLocationCommandTest
{
    private readonly ILocationCommandRepository _mockLocationCommandRepository;
    private readonly ILocationCommandRepository _mockLocationCommandRepositoryException;
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

    public DeleteLocationCommandTest()
    {
        _mockLocationCommandRepository = MockLocationCommandRepository.GetLocationCommandRepository();
        _mockLocationCommandRepositoryException = MockLocationCommandRepository.GetLocationCommandRepositoryException();
    }

    [Fact]
    public async Task DeleteLocationCommandShouldSucceed()
    {
        var command = new DeleteLocationCommand(Guid.NewGuid());

        var handler = new DeleteLocationCommandHandler(
            _mockLocationCommandRepository);

        var result = await handler.Handle(command, _cancellationToken);

        Assert.Equal(Unit.Value, result);
    }

    [Fact]
    public async Task DeleteLocationCommandShouldThrow()
    {
        var command = new DeleteLocationCommand(Guid.NewGuid());

        var handler = new DeleteLocationCommandHandler(
            _mockLocationCommandRepositoryException);

        var exception = await Record.ExceptionAsync(() =>
            handler.Handle(command, _cancellationToken));

        Assert.IsType<NotFoundException>(exception);
    }
}