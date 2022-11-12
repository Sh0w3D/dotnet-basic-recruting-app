using MatchDataManager.Application.Common.Exceptions.Shared;
using MatchDataManager.Application.Common.Interfaces.Repositories.Command;
using MatchDataManager.Domain.Entities;
using Moq;

namespace MatchDataManaget.IntegrationTests.Teams.Mocks;

internal static class MockTeamCommandRepository
{
    internal static ITeamCommandRepository GetTeamRepository()
    {
        var mockTeamCommandRepository = new Mock<ITeamCommandRepository>();

        mockTeamCommandRepository.Setup(setup =>
            setup.CreateTeamAsync(
                It.IsAny<Team>(),
                It.IsAny<CancellationToken>()));
        mockTeamCommandRepository.Setup(setup =>
            setup.UpdateTeamAsync(
                It.IsAny<Team>(),
                It.IsAny<CancellationToken>()));

        mockTeamCommandRepository.Setup(setup =>
            setup.DeleteTeamAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()));

        return mockTeamCommandRepository.Object;
    }

    internal static ITeamCommandRepository GetTeamRepositoryException()
    {
        var mockTeamCommandRepository = new Mock<ITeamCommandRepository>();

        mockTeamCommandRepository.Setup(setup =>
                setup.CreateTeamAsync(
                    It.IsAny<Team>(),
                    It.IsAny<CancellationToken>()))
            .Throws(new SaveToDatabaseException());

        mockTeamCommandRepository.Setup(setup =>
                setup.UpdateTeamAsync(
                    It.IsAny<Team>(),
                    It.IsAny<CancellationToken>()))
            .Throws(new NotFoundException());

        mockTeamCommandRepository.Setup(setup =>
                setup.UpdateTeamAsync(
                    It.IsAny<Team>(),
                    It.IsAny<CancellationToken>()))
            .Throws(new SaveToDatabaseException());

        mockTeamCommandRepository.Setup(setup =>
                setup.DeleteTeamAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .Throws(new SaveToDatabaseException());

        return mockTeamCommandRepository.Object;
    }
}