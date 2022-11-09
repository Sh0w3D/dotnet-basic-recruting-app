namespace MatchDataManager.Contracts.Requests.Team;

public record UpdateTeamRequest(
    Guid Id,
    string Name,
    string CoachName);