namespace MatchDataManager.Contracts.Responses;

public record LocationResponse(
    Guid Id,
    string Name,
    string City);