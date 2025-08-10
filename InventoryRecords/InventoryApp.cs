using System;
using System.IO;

public class InventoryApp
{
    private InventoryLogger<InventoryItem> _logger;

    public InventoryApp()
    {
        _logger = new InventoryLogger<InventoryItem>("inventory.json");
    }

    public void SeedSampleData()
    {
        _logger.Add(new InventoryItem(1, "Notebook", 30, DateTime.Now));
        _logger.Add(new InventoryItem(2, "Pencil", 95, DateTime.Now));
        _logger.Add(new InventoryItem(3, "Marker", 40, DateTime.Now));
        _logger.Add(new InventoryItem(3, "Crayon", 65, DateTime.Now));
        _logger.Add(new InventoryItem(3, "Duster", 220, DateTime.Now));
           _logger.Add(new InventoryItem(3, "Pencil_Sharpener", 90, DateTime.Now));
    }

    public void SaveData()
    {
        _logger.SaveToFile();
        Console.WriteLine("Inventory saved to file.");
    }

    public void LoadData()
    {
        _logger.LoadFromFile();
        Console.WriteLine("Inventory loaded from file.");
    }

    public void PrintAllItems()
    {
        var items = _logger.GetAll();
        if (items.Count == 0)
        {
            Console.WriteLine("No inventory items to display.");
        }
        else
        {
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded}");
            }
        }
    }

    public static void Main()
    {
        var app = new InventoryApp();

        // Simulate one session: Seed, Save, Clear, Load, Print
        app.SeedSampleData();
        app.SaveData();

        Console.WriteLine("\nClearing memory and reloading data...\n");
        app = new InventoryApp();
        app.LoadData();
        app.PrintAllItems();
    }
}
