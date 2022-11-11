using MatchDataManager.Domain.Entities.Shared;

namespace MatchDataManager.Domain.Entities;

public class Location : BaseEntity
{
    public Location()
    {
    }

    public Location(string name, string city)
    {
        Id = Guid.NewGuid();
        Name = name;
        City = city;
    }

    public Location(Guid id, string name, string city)
    {
        Id = id;
        Name = name;
        City = city;
    }

    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
}