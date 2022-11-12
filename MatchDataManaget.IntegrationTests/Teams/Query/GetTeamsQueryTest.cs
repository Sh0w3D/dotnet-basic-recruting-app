using MatchDataManager.Application.Common.Interfaces.Repositories.Query;
using MatchDataManager.Application.Teams.Query.GetTeams;
using MatchDataManaget.IntegrationTests.Teams.Mocks;
using Xunit;

namespace MatchDataManaget.IntegrationTests.Teams.Query;

public class GetTeamsQueryTest
{
    private readonly ITeamQueryRepository _mockTeamQueryRepository;
    private readonly ITeamQueryRepository _mockTeamQueryRepositoryNull;
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

    public GetTeamsQueryTest()
    {
        _mockTeamQueryRepository = MockTeamQueryRepository.GetTeamQueryRepository();
        _mockTeamQueryRepositoryNull = MockTeamQueryRepository.GetTeamQueryRepositoryNull();
    }

    [Fact]
    public async Task GetTeamsQueryShouldSucceed()
    {
        var query = new GetTeamsQuery();

        var handler = new GetTeamsQueryHandler(_mockTeamQueryRepository);

        var result = await handler.Handle(query, _cancellationToken);

        Assert.NotEmpty(result);

        foreach (var team in result)
        {
            var expected = TeamTestData.TeamData.Find(x => x.Id == team.Id);

            Assert.NotNull(team);
            Assert.NotNull(expected);
            Assert.Equal(expected!.Name, team.Name);
            Assert.Equal(expected.CoachName, team.CoachName);
        }
    }

    [Fact]
    public async Task GetTeamsQueryShouldReturnEmpty()
    {
        var query = new GetTeamsQuery();

        var handler = new GetTeamsQueryHandler(_mockTeamQueryRepositoryNull);

        var result = await handler.Handle(query, _cancellationToken);

        Assert.Empty(result);
    }
}