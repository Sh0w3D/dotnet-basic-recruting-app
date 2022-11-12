using MatchDataManager.Application.Common.Exceptions.Shared;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Application.Locations.Command.CreateLocation;
using MatchDataManaget.IntegrationTests.Locations.Mocks;
using Xunit;

namespace MatchDataManaget.IntegrationTests.Locations.Command;

public class CreateLocationCommandTest
{
    private readonly ILocationCommandRepository _mockLocationCommandRepository;
    private readonly ILocationCommandRepository _mockLocationCommandRepositoryException;
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

    public CreateLocationCommandTest()
    {
        _mockLocationCommandRepository = MockLocationCommandRepository.GetLocationCommandRepository();
        _mockLocationCommandRepositoryException = MockLocationCommandRepository.GetLocationCommandRepositoryException();
    }

    [Fact]
    public async Task CreateLocationCommandShouldSucceed()
    {
        var command = new CreateLocationCommand("SR", "Rybnik");

        var handler = new CreateLocationCommandHandler(
            _mockLocationCommandRepository);

        var result = await handler.Handle(command, _cancellationToken);

        Assert.IsType<Guid>(result.Id);
        Assert.Equal(result.Name, command.Name);
        Assert.Equal(result.City, command.City);
    }

    [Fact]
    public async Task CreateLocationCommandShouldThrowException()
    {
        var command = new CreateLocationCommand("SR", "Rybnik");

        var handler = new CreateLocationCommandHandler(
            _mockLocationCommandRepositoryException);

        var exception = await Record.ExceptionAsync(() =>
            handler.Handle(command, _cancellationToken));

        Assert.IsType<SaveToDatabaseException>(exception);
    }
}