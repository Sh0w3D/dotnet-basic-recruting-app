using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Application.Locations.Query.GetLocations;
using MatchDataManaget.IntegrationTests.Locations.Mocks;
using Xunit;

namespace MatchDataManaget.IntegrationTests.Locations.Query;

public class GetLocationsQueryTest
{
    private readonly ILocationQueryRepository _mockLocationQueryRepository;
    private readonly ILocationQueryRepository _mockLocationQueryRepositoryNullObject;
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

    public GetLocationsQueryTest()
    {
        _mockLocationQueryRepository = MockLocationQueryRepository.GetLocationRepository();
        _mockLocationQueryRepositoryNullObject = MockLocationQueryRepository.GetLocationRepositoryNullObject();
    }

    [Fact]
    public async Task GetLocationsQueryShouldSucceed()
    {
        var query = new GetLocationsQuery();

        var handler = new GetLocationsQueryHandler(
            _mockLocationQueryRepository);

        var result = await handler.Handle(query, _cancellationToken);

        Assert.NotEmpty(result);

        foreach (var location in result)
        {
            var expected = LocationTestData.LocationData
                .First(x => x.Id == location.Id);

            Assert.NotNull(location);
            Assert.Equal(expected.Name, location.Name);
            Assert.Equal(expected.City, location.City);
        }
    }

    [Fact]
    public async Task GetLocationsQueryShouldReturnNull()
    {
        var query = new GetLocationsQuery();

        var handler = new GetLocationsQueryHandler(_mockLocationQueryRepositoryNullObject);

        var result = await handler.Handle(query, _cancellationToken);

        Assert.Empty(result);
    }
}