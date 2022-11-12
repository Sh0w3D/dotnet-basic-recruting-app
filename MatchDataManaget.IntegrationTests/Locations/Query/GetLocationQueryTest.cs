using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Application.Locations.Query.GetLocation;
using MatchDataManaget.IntegrationTests.Locations.Mocks;
using Xunit;

namespace MatchDataManaget.IntegrationTests.Locations.Query;

public class GetLocationQueryTest
{
    private readonly ILocationQueryRepository _mockLocationQueryRepository;
    private readonly ILocationQueryRepository _mockLocationQueryRepositoryNullObject;
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

    public GetLocationQueryTest()
    {
        _mockLocationQueryRepository = MockLocationQueryRepository.GetLocationRepository();
        _mockLocationQueryRepositoryNullObject = MockLocationQueryRepository.GetLocationRepositoryNullObject();
    }

    [Fact]
    public async Task GetLocationQueryShouldSucceed()
    {
        var expected = LocationTestData.LocationData[0];

        var query = new GetLocationByIdQuery(expected.Id);

        var handler = new GetLocationByIdQueryHandler(_mockLocationQueryRepository);

        var result = await handler.Handle(query, _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(expected.Id, result!.Id);
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.City, result.City);
    }

    [Fact]
    public async Task GetLocationByIdQueryShouldReturnNull()
    {
        var query = new GetLocationByIdQuery(Guid.NewGuid());

        var handler = new GetLocationByIdQueryHandler(
            _mockLocationQueryRepositoryNullObject);

        var result = await handler.Handle(query, _cancellationToken);

        Assert.Null(result);
    }
}