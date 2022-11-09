namespace MatchDataManager.Domain.Models;

public class Team : Entity
{
    public string Name { get; set; } = null!;

    public string CoachName { get; set; } = null!;
}