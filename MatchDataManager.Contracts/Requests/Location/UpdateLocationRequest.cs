namespace MatchDataManager.Contracts.Requests.Location;

public record UpdateLocationRequest(
    Guid Id,
    string Name,
    string City);