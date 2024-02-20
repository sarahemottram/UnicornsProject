namespace UnicornsInventorySystem.Entities;

public class Supplier : IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
