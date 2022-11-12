using MatchDataManager.Application.Common.Exceptions.Shared;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Application.Locations.Command.UpdateLocation;
using MatchDataManaget.IntegrationTests.Locations.Mocks;
using MediatR;
using Xunit;

namespace MatchDataManaget.IntegrationTests.Locations.Command;

public class UpdateLocationCommandTest
{
    private readonly ILocationCommandRepository _mockLocationCommandRepository;
    private readonly ILocationCommandRepository _mockLocationCommandRepositoryException;
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

    public UpdateLocationCommandTest()
    {
        _mockLocationCommandRepository = MockLocationCommandRepository.GetLocationCommandRepository();
        _mockLocationCommandRepositoryException = MockLocationCommandRepository.GetLocationCommandRepositoryException();
    }

    [Fact]
    public async Task UpdateLocationCommandShouldSucceed()
    {
        var command = new UpdateLocationCommand(Guid.NewGuid(), "SRB", "Jankowice");

        var handler = new UpdateLocationCommandHandler(
            _mockLocationCommandRepository);

        var result = await handler.Handle(command, _cancellationToken);

        Assert.Equal(Unit.Value, result);
    }

    [Fact]
    public async Task UpdateLocationCommandShouldThrowSaveToDataBaseException()
    {
        var command = new UpdateLocationCommand(Guid.NewGuid(), "S_R", "Rybnik");

        var handler = new UpdateLocationCommandHandler(
            _mockLocationCommandRepositoryException);

        var exception = await Record.ExceptionAsync(() =>
            handler.Handle(command, _cancellationToken));

        Assert.IsType<SaveToDatabaseException>(exception);
    }
}