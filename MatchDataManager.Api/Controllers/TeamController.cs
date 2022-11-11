using MatchDataManager.Application.Teams.Command.CreateTeam;
using MatchDataManager.Application.Teams.Command.DeleteTeam;
using MatchDataManager.Application.Teams.Command.UpdateTeam;
using MatchDataManager.Application.Teams.Query.GetTeam;
using MatchDataManager.Application.Teams.Query.GetTeams;
using MatchDataManager.Contracts.Requests.Team;
using MatchDataManager.Contracts.Responses;
using MatchDataManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

public class TeamController : ApiControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTeamById(Guid id)
    {
        var team = await Mediator.Send(new GetTeamByIdQuery(id));

        return Ok(ToTeamResponse(team));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTeams()
    {
        var teams = await Mediator.Send(new GetTeamsQuery());

        return Ok(ToTeamResponseList(teams));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam(CreateTeamRequest request)
    {
        var result = await Mediator.Send(new CreateTeamCommand(
            request.Name,
            request.CoachName));

        return CreatedAtAction(
            nameof(CreateTeam),
            new { id = result.Id },
            ToTeamResponse(result));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTeam(Guid id, UpdateTeamRequest request)
    {
        await Mediator.Send(new UpdateTeamCommand(
            id,
            request.Name,
            request.CoachName));

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTeam(Guid id)
    {
        await Mediator.Send(new DeleteTeamCommand(id));

        return NoContent();
    }

    private static List<TeamResponse> ToTeamResponseList(List<Team> teams)
    {
        return teams.ConvertAll(team => ToTeamResponse(team)!);
    }

    private static TeamResponse? ToTeamResponse(Team? team)
    {
        if (team is null) return null;

        return new TeamResponse(
            team.Id,
            team.Name,
            team.CoachName
        );
    }
}