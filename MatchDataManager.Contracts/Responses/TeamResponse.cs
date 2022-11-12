namespace MatchDataManager.Contracts.Responses;

public sealed record TeamResponse(
    Guid Id,
    string Name,
    string? CoachName);