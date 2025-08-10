using System;

public class WareHouseManager
{
    private InventoryRepository<ElectronicItem> _electronics = new InventoryRepository<ElectronicItem>();
    private InventoryRepository<GroceryItem> _groceries = new InventoryRepository<GroceryItem>();

    public void SeedData()
    {
        _electronics.AddItem(new ElectronicItem(1, "Laptop", 10, "Dell", 24));
        _electronics.AddItem(new ElectronicItem(2, "Smartphone", 20, "Samsung", 12));

        _groceries.AddItem(new GroceryItem(1, "Rice", 100, DateTime.Now.AddMonths(6)));
        _groceries.AddItem(new GroceryItem(2, "Milk", 50, DateTime.Now.AddDays(10)));
    }

    public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
    {
        foreach (var item in repo.GetAllItems())
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}");
        }
    }

    public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
    {
        try
        {
            var item = repo.GetItemById(id);
            repo.UpdateQuantity(id, item.Quantity + quantity);
            Console.WriteLine($"Stock updated for {item.Name}. New Quantity: {item.Quantity}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] {ex.Message}");
        }
    }

    public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
    {
        try
        {
            repo.RemoveItem(id);
            Console.WriteLine($"Item with ID {id} removed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] {ex.Message}");
        }
    }

    public static void Main()
    {
        var manager = new WareHouseManager();
        manager.SeedData();

        Console.WriteLine("\n Grocery Items:");
        manager.PrintAllItems(manager._groceries);

        Console.WriteLine("\n Electronic Items:");
        manager.PrintAllItems(manager._electronics);

        // Test error handling
        try { manager._electronics.AddItem(new ElectronicItem(1, "Tablet", 5, "Apple", 12)); }
        catch (Exception ex) { Console.WriteLine($"[Add Error] {ex.Message}"); }

        manager.RemoveItemById(manager._electronics, 999); // non-existent
        manager.IncreaseStock(manager._groceries, 2, -10); // invalid quantity
    }
}
