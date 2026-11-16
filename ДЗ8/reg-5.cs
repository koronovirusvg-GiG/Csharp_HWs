using System;
using System.IO;
using System.Text.Json;

public class Char
{
    public string? Name { get; set; }
    public int Level { get; set; }
    public double Health { get; set; }
    public string[]? Inventory { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Char hero = new Char
        {
            Name = "Polina",
            Level = 15,
            Health = 67.42,
            Inventory = new string[] { "Sword", "Potion", "Shield" }
        };

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string jsonString = JsonSerializer.Serialize(hero, options);


        string filePath = Path.Combine(AppContext.BaseDirectory, "save.json");


        File.WriteAllText(filePath, jsonString);


        string jsonFromFile = File.ReadAllText(filePath);
        Char loadedHero = JsonSerializer.Deserialize<Char>(jsonFromFile)!;


        Console.WriteLine("\n\n");
        Console.WriteLine($"Имя: {loadedHero.Name}");
        Console.WriteLine($"Уровень: {loadedHero.Level}");
        Console.WriteLine($"Здоровье: {loadedHero.Health}");
        Console.WriteLine($"Инвентарь: {string.Join(" + ", loadedHero.Inventory!)}");
        Console.WriteLine("\n\n");
    }
}
