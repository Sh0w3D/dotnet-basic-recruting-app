using MatchDataManager.Application.Teams.Command.Create;
using MatchDataManager.Application.Teams.Command.Delete;
using MatchDataManager.Application.Teams.Command.Update;
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
    public async Task<IActionResult> Get(Guid id)
    {
        var team = await Mediator.Send(new GetTeamByIdQuery(id));

        return Ok(ToTeamResponse(team));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var teams = await Mediator.Send(new GetTeamsQuery());

        return Ok(ToTeamResponseList(teams));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateTeamRequest request)
    {
        var result = await Mediator.Send(new CreateTeamCommand(
            Name: request.Name,
            CoachName: request.CoachName));

        return CreatedAtAction(
            nameof(Create),
            new { id = result.Id },
            ToTeamResponse(result));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTeamRequest request)
    {
        await Mediator.Send(new UpdateTeamCommand(
            Id: id,
            Name: request.Name,
            CoachName: request.CoachName));

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
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
            Id: team.Id,
            Name: team.Name,
            CoachName: team.CoachName
        );
    }
}