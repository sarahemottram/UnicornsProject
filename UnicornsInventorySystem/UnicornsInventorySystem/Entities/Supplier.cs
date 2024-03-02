namespace UnicornsInventorySystem.Entities;

public record Supplier : IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
