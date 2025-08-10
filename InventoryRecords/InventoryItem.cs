using System;

public interface IInventoryEntity
{
    int Id { get; }
}

// You can use positional or property-based record syntax
public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;
