namespace UnicornsInventorySystem.Entities;

public class Product : IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required string Sku { get; set; }
    public required int QuantityInStock { get; set; }
    public required int CategoryId { get; set; }
}