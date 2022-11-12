namespace MatchDataManager.Contracts.Requests.Location;

public sealed record CreateLocationRequest(
    string? Name,
    string? City);