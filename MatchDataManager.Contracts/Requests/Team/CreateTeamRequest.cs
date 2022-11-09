namespace MatchDataManager.Contracts.Requests.Team;

public record CreateTeamRequest(
    string Name,
    string CoachName);