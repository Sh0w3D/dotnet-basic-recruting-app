namespace MatchDataManager.Contracts.Requests.Team;

public sealed record CreateTeamRequest(
    string? Name,
    string? CoachName);