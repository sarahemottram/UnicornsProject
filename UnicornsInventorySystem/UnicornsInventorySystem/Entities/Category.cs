﻿namespace UnicornsInventorySystem.Entities;

public record Category : IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}
