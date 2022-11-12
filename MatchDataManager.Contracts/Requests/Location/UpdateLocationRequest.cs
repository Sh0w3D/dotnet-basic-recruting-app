namespace MatchDataManager.Contracts.Requests.Location;

public sealed record UpdateLocationRequest(
    string? Name,
    string? City);