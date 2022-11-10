using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using Moq;

namespace MatchDataManager.UnitTests.Team.Mocks;

internal static class MockTeamQueryRepository
{
    internal static ITeamQueryRepository GetTeamUniqueName(bool uniqueName)
    {
        var mockTeamQueryRepository = new Mock<ITeamQueryRepository>();

        mockTeamQueryRepository.Setup(setup =>
                setup.UniqueNameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(uniqueName);

        return mockTeamQueryRepository.Object;
    }
}