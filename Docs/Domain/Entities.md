## Entities in Domain

#### Location
---
###### Code representation
```csharp
{
    public Guid Id { get; set; } // implemented from BaseEntity
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
}
```
###### Json representation
```json
{
    "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
    "name": "Test Name",
    "city": "Test City"
}
```

#### Team
---
###### Code representation
```csharp
{
    public Guid Id { get; set; } // implemented from BaseEntity
    public string Name { get; set; } = null!;
    public string? CoachName { get; set; }
}
```
###### Json representation
```json
{
    "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
    "name": "Test Name",
    "coachNAme": "Test Coach"
}
```