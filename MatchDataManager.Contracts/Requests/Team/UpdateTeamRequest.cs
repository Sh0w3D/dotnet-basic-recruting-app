namespace MatchDataManager.Contracts.Requests.Team;

public record UpdateTeamRequest(
    string Name,
    string? CoachName);