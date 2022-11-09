using MatchDataManager.Application.Locations.Command.CreateLocation;
using MatchDataManager.Application.Locations.Query.GetLocation;
using MatchDataManager.Application.Locations.Query.GetLocations;
using MatchDataManager.Contracts.Requests.Location;
using MatchDataManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MatchDataManager.Contracts.Responses;
using MatchDataManager.Application.Locations.Command.UpdateLocation;
using MatchDataManager.Application.Locations.Command.DeleteLocation;

namespace MatchDataManager.Api.Controllers;

public class LocationController : ApiControllerBase
{
    [HttpGet("id:guid")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await Mediator.Send(new GetLocationByIdQuery(id));

        return Ok(ToLocationResponse(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetLocationsQuery());

        return Ok(ToLocationResponseList(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLocationRequest request)
    {
        var result = await Mediator.Send(new CreateLocationCommand(
            Name: request.Name,
            City: request.City));

        return CreatedAtAction(
            nameof(Create),
            new { id = result.Id },
            ToLocationResponse(result));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateLocationRequest request)
    {
        await Mediator.Send(new UpdateLocationCommand(
            Id: id,
            Name: request.Name,
            City: request.City));

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteLocationCommand(id));

        return NoContent();
    }

    private static List<LocationResponse> ToLocationResponseList(List<Location> locations)
    {
        return locations.ConvertAll(location => ToLocationResponse(location)!);
    }
    private static LocationResponse? ToLocationResponse(Location? location)
    {
        if (location is null)
            return null;

        return new LocationResponse(
            Id: location.Id,
            Name: location.Name,
            City: location.City);
    }
}