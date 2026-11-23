using System;
using System.Collections.Generic;

public enum Rarity
{
    Common,
    Rare,
    Legendary
}

public interface IUsableItem
{
    string Name { get; }
    string Description { get; }
    Rarity Rarity { get; }

    void Use(Player player);
}

public class Player
{
    public string Name { get; set; }
    public int HP { get; set; }
    public int Damage { get; set; }

    public Player(string name, int hp = 100, int damage = 10)
    {
        Name = name;
        HP = hp;
        Damage = damage;
    }

    public void Heal(int amount)
    {
        HP += amount;
    }

    public void IncreaseDamage(int amount)
    {
        Damage += amount;
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
    }
}

public class HealthPotion : IUsableItem
{
    public string Name => "Зелье здоровья";
    public string Description => "Восстанавливает 50 HP";
    public Rarity Rarity => Rarity.Common;

    public void Use(Player player)
    {
        player.Heal(50);

        Console.WriteLine(
            $"Вы выпили зелье и восстановили 50 HP. Текущее HP: {player.HP}"
        );
    }
}

public class Sword : IUsableItem
{
    public string Name => "Сворд";
    public string Description => "Увеличивает урон на 10";
    public Rarity Rarity => Rarity.Rare;

    public void Use(Player player)
    {
        player.IncreaseDamage(10);

        Console.WriteLine(
            $"Вы использовали сворд. Урон увеличен на 10. Текущий урон: {player.Damage}"
        );
    }
}

public class AncientScroll : IUsableItem
{
    public string Name => "Древний свиток";
    public string Description => "Наносит 20 урона, но увеличивает урон на 30";
    public Rarity Rarity => Rarity.Legendary;

    public void Use(Player player)
    {
        player.TakeDamage(20);
        player.IncreaseDamage(30);

        Console.WriteLine(
            $"Вы использовали древний свиток! Получено 20 урона, урон увеличен на 30."
        );
    }
}

public class Program
{
    public static void Main()
    {
        Player player = new Player("Полина");

        List<IUsableItem> items = new List<IUsableItem>
        {
            new HealthPotion(),
            new Sword(),
            new AncientScroll()
        };

        foreach (IUsableItem item in items)
        {
            Console.WriteLine($"\nИспользуется: {item.Name}");
            item.Use(player);
        }

        Console.WriteLine("\n--- Итоговые характеристики ---");

        Console.WriteLine($"Имя: {player.Name}");
        Console.WriteLine($"HP: {player.HP}");
        Console.WriteLine($"Урон: {player.Damage}");

        Console.WriteLine(
            player.HP > 0 ? "Состояние: жив" : "Состояние: мёртв"
        );
    }
}
