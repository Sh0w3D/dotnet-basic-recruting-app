using MatchDataManager.Application.Common.Exceptions.Shared;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Domain.Entities;
using Moq;

namespace MatchDataManaget.IntegrationTests.Locations.Mocks;

internal static class MockLocationCommandRepository
{
    internal static ILocationCommandRepository GetLocationCommandRepository()
    {
        var mockLocationCommandRespository = new Mock<ILocationCommandRepository>();

        mockLocationCommandRespository.Setup(setup =>
            setup.CreateAsync(
                It.IsAny<Location>(),
                It.IsAny<CancellationToken>()));

        mockLocationCommandRespository.Setup(setup =>
            setup.UpdateLocationAsync(
                It.IsAny<Location>(),
                It.IsAny<CancellationToken>()));

        mockLocationCommandRespository.Setup(setup =>
            setup.DeleteLocationAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()));

        return mockLocationCommandRespository.Object;
    }
    internal static ILocationCommandRepository GetLocationCommandRepositoryException()
    {
        var mockLocationCommandRespository = new Mock<ILocationCommandRepository>();

        mockLocationCommandRespository.Setup(setup =>
                setup.CreateAsync(
                    It.IsAny<Location>(),
                    It.IsAny<CancellationToken>()))
            .Throws(new SaveToDatabaseException());

        mockLocationCommandRespository.Setup(setup =>
                setup.DeleteLocationAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .Throws(new NotFoundException());

        mockLocationCommandRespository.Setup(setup =>
                setup.UpdateLocationAsync(
                    It.IsAny<Location>(),
                    It.IsAny<CancellationToken>()))
            .Throws(new NotFoundException());

        mockLocationCommandRespository.Setup(setup =>
                setup.UpdateLocationAsync(
                    It.IsAny<Location>(),
                    It.IsAny<CancellationToken>()))
            .Throws(new SaveToDatabaseException());

        return mockLocationCommandRespository.Object;
    }
}