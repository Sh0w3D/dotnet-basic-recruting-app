using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using Moq;

namespace MatchDataManager.UnitTests.Location.Mocks;

internal static class MockLocationQueryRepository
{
    internal static ILocationQueryRepository GetLocationUniqueName(bool uniqueName)
    {
        var mockLocationQueryRepository = new Mock<ILocationQueryRepository>();

        mockLocationQueryRepository
            .Setup(setup =>
                setup.UniqueNameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(uniqueName);
        
        return mockLocationQueryRepository.Object;
    }
}