using MatchDataManager.Application.Locations.Command.CreateLocation;
using MatchDataManager.Application.Locations.Command.DeleteLocation;
using MatchDataManager.Application.Locations.Command.UpdateLocation;
using MatchDataManager.Application.Locations.Query.GetLocation;
using MatchDataManager.Application.Locations.Query.GetLocations;
using MatchDataManager.Contracts.Requests.Location;
using MatchDataManager.Contracts.Responses;
using MatchDataManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MatchDataManager.Api.Controllers;

public class LocationController : ApiControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetLocationById(Guid id)
    {
        var result = await Mediator.Send(new GetLocationByIdQuery(id));

        return Ok(ToLocationResponse(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLocations()
    {
        var result = await Mediator.Send(new GetLocationsQuery());

        return Ok(ToLocationResponseList(result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateLocation(CreateLocationRequest request)
    {
        var result = await Mediator.Send(new CreateLocationCommand(
            request.Name,
            request.City));

        return CreatedAtAction(
            nameof(CreateLocation),
            new { id = result.Id },
            ToLocationResponse(result));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateLocation(Guid id, UpdateLocationRequest request)
    {
        await Mediator.Send(new UpdateLocationCommand(
            id,
            request.Name,
            request.City));

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteLocation(Guid id)
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
            location.Id,
            location.Name,
            location.City);
    }
}