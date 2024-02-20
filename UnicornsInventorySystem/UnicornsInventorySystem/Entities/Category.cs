namespace UnicornsInventorySystem.Entities;

public class Category : IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}
