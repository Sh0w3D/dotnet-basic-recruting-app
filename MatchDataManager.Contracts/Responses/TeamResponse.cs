namespace MatchDataManager.Contracts.Responses;

public record TeamResponse(
    Guid Id,
    string Name,
    string? CoachName);