using MatchDataManager.Domain.Entities.Shared;

namespace MatchDataManager.Domain.Entities;

public class Team : BaseEntity
{
    public Team() { }

    public Team(string name, string coachName)
    {
        Id = Guid.NewGuid();
        Name = name;
        CoachName = coachName;
    }
    public Team(Guid id, string name, string coachName)
    {
        Id = id;
        Name = name;
        CoachName = coachName;
    }
    public string Name { get; set; } = null!;
    public string CoachName { get; set; } = null!;
}