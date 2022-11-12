using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Application.Teams.Query.GetTeam;
using MatchDataManaget.IntegrationTests.Teams.Mocks;
using Xunit;

namespace MatchDataManaget.IntegrationTests.Teams.Query;

public class GetTeamQueryTest
{
    private readonly ITeamQueryRepository _mockTeamQueryRepository;
    private readonly ITeamQueryRepository _mockTeamQueryRepositoryNull;
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

    public GetTeamQueryTest()
    {
        _mockTeamQueryRepository = MockTeamQueryRepository.GetTeamQueryRepository();
        _mockTeamQueryRepositoryNull = MockTeamQueryRepository.GetTeamQueryRepositoryNull();
    }

    [Fact]
    public async Task GetTeamQueryShouldSucceed()
    {
        var expected = TeamTestData.TeamData[0];
        var query = new GetTeamByIdQuery(expected.Id);

        var handler = new GetTeamByIdQueryHandler(
            _mockTeamQueryRepository);

        var result = await handler.Handle(query, _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(expected.Name, result!.Name);
        Assert.Equal(expected.CoachName, result.CoachName);
    }

    [Fact]
    public async Task GetTeamQueryShouldReturnNull()
    {
        var query = new GetTeamByIdQuery(Guid.NewGuid());

        var handler = new GetTeamByIdQueryHandler(
            _mockTeamQueryRepositoryNull);

        var result = await handler.Handle(query, _cancellationToken);

        Assert.Null(result);
    }
}