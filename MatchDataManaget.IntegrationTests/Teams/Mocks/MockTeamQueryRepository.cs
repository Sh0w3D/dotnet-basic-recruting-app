using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Domain.Entities;
using Moq;

namespace MatchDataManaget.IntegrationTests.Teams.Mocks;

internal static class MockTeamQueryRepository
{
    internal static ITeamQueryRepository GetTeamQueryRepository()
    {
        var mockTeamQueryRepository = new Mock<ITeamQueryRepository>();

        mockTeamQueryRepository.Setup(setup =>
                setup.GetTeamAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(TeamTestData.TeamData[0]);

        mockTeamQueryRepository.Setup(setup =>
                setup.GetTeamsAsync(
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(TeamTestData.TeamData);

        return mockTeamQueryRepository.Object;
    }
    internal static ITeamQueryRepository GetTeamQueryRepositoryNull()
    {
        var mockTeamQueryRepository = new Mock<ITeamQueryRepository>();

        mockTeamQueryRepository.Setup(setup =>
                setup.GetTeamAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()));

        mockTeamQueryRepository.Setup(setup =>
                setup.GetTeamsAsync(
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Team>());

        return mockTeamQueryRepository.Object;
    }
}