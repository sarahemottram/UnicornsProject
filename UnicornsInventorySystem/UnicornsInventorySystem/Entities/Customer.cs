namespace UnicornsInventorySystem.Entities;

public record Customer : IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
