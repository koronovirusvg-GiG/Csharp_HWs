using System;
using System.IO;
using System.Text.Json;

public class GameSettings
{
    public string? PlayerName { get; set; }
    public int Difficulty { get; set; }
    public int MusicVolume { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "settings.json");

        if (File.Exists(filePath))
        {
            string jsonFromFile = File.ReadAllText(filePath);
            GameSettings settings = JsonSerializer.Deserialize<GameSettings>(jsonFromFile)!;

            Console.WriteLine("Текущие настройки");
            Console.WriteLine($"Имя игрока: {settings.PlayerName}");
            Console.WriteLine($"Сложность: {settings.Difficulty}");
            Console.WriteLine($"Громкость музыки: {settings.MusicVolume}");

            
            Console.Write("\nСбросить настройки? (y/n):");
            string? answer = Console.ReadLine();

            if (answer?.ToLower() == "y")
            {
                File.Delete(filePath);
                Console.WriteLine("Настройки сброшены");
            }
            else
            {
                Console.WriteLine("Настройки оставлены без изменений");
            }
        }
        else
        {
            Console.WriteLine("Файл настроек не найден. Создаём новые настройки");

            Console.Write("Введите имя игрока:");
            string? name = Console.ReadLine();

            Console.Write("Введите сложность (1 — легко, 2 — нормально, 3 — сложно):");
            int difficulty = int.Parse(Console.ReadLine()!);

            Console.Write("Введите громкость музыки (0–100):");
            int volume = int.Parse(Console.ReadLine()!);

            GameSettings newSettings = new GameSettings
            {
                PlayerName = name,
                Difficulty = difficulty,
                MusicVolume = volume
            };

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(newSettings, options);
            File.WriteAllText(filePath, jsonString);

            Console.WriteLine("\nНастройки успешно сохранены!");
        }
    }
}
