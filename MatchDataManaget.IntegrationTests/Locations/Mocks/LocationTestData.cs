using MatchDataManager.Domain.Entities;

namespace MatchDataManaget.IntegrationTests.Locations.Mocks;

public static class LocationTestData
{
    public static readonly List<Location> LocationData = new()
    {
        new Location(Guid.NewGuid(), "SRC", "Rudy"),
        new Location(Guid.NewGuid(), "SR", "Rybnik")
    };
}