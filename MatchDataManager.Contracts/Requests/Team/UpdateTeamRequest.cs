// using System.ComponentModel.DataAnnotations;

namespace MatchDataManager.Contracts.Requests.Team;

public record UpdateTeamRequest(
    // [MaxLength(255)]
    // [Required(AllowEmptyStrings = false, ErrorMessage = "string empty")]
    string Name,
    // [MaxLength(55)]
    string? CoachName);