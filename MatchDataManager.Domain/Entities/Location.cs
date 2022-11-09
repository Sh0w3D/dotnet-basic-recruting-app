using MatchDataManager.Domain.Entities.Shared;

namespace MatchDataManager.Domain.Entities;

public class Location : BaseEntity
{
    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;
}