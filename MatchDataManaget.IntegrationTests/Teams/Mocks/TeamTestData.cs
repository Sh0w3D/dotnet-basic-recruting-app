using MatchDataManager.Domain.Entities;

namespace MatchDataManaget.IntegrationTests.Teams.Mocks;

public static class TeamTestData
{
    public static readonly List<Team> TeamData = new()
    {
        new Team(Guid.NewGuid(), "LKS Buk Rudy", "Kamil"),
        new Team(Guid.NewGuid(), "LKS Buk Rudy", "Kazimierz")
    };
}