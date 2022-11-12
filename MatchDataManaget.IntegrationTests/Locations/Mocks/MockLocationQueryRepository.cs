using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Entities;
using Moq;

namespace MatchDataManaget.IntegrationTests.Locations.Mocks;

internal static class MockLocationQueryRepository
{
    internal static ILocationQueryRepository GetLocationRepository()
    {
        var mockLocationQueryRepository = new Mock<ILocationQueryRepository>();

        mockLocationQueryRepository.Setup(setup =>
                setup.GetLocationAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(LocationTestData.LocationData[0]);

        mockLocationQueryRepository.Setup(setup =>
                setup.GetLocationsAsync(
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(LocationTestData.LocationData);

        return mockLocationQueryRepository.Object;
    }

    internal static ILocationQueryRepository GetLocationRepositoryNullObject()
    {
        var mockLocationQueryRepository = new Mock<ILocationQueryRepository>();

        mockLocationQueryRepository.Setup(setup =>
            setup.GetLocationAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()));

        mockLocationQueryRepository.Setup(setup =>
                setup.GetLocationsAsync(
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Location>());

        return mockLocationQueryRepository.Object;
    }
}