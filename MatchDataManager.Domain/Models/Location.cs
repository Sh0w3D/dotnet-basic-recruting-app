namespace MatchDataManager.Domain.Models;

public class Location : Entity
{
    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;
}