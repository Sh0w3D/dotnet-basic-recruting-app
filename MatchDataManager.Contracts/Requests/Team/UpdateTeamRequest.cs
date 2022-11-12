namespace MatchDataManager.Contracts.Requests.Team;

public sealed record UpdateTeamRequest(
    string? Name,
    string? CoachName);