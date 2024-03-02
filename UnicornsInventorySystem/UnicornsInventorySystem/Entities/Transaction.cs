namespace UnicornsInventorySystem.Entities;

public record Transaction : IEntity
{
    public int Id { get; set; }
    public required int ProductId { get; set; }
    public required int CustomerId { get; set; }
    public required decimal Price { get; set; }
    public required DateTime Date { get; set; }
    public required TransactionType Type { get; set; }
}