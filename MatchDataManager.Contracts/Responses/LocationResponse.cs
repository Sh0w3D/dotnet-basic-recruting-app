namespace MatchDataManager.Contracts.Responses;

public sealed record LocationResponse(
    Guid Id,
    string Name,
    string City);