using System;
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

        Console.WriteLine(jsonString);

        Console.WriteLine();
        Console.WriteLine();


        Char restoredHero = JsonSerializer.Deserialize<Char>(jsonString)!;

        Console.WriteLine($"Имя: {restoredHero.Name}");
        Console.WriteLine($"Количество предметов в инвентаре: {restoredHero?.Inventory?.Length}");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
    }
}
