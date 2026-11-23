using System;

public interface IDamageCalculator
{
    int CalculateDamage(int baseDamage, int targetArmor);
}

// Физ
public class PhysicalDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = baseDamage - targetArmor / 2;

        return Math.Max(1, damage);
    }
}

// Маг
public class MagicDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = baseDamage - targetArmor / 4;

        if (targetArmor == 0)
        {
            damage *= 2;
        }

        return Math.Max(1, damage);
    }
}

// Огонь
public class FireDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = baseDamage - targetArmor / 10;

        if (baseDamage > 50)
        {
            damage += 10;
        }

        return Math.Max(1, damage);
    }
}


public class Polina
{
    public string Name { get; set; }
    public int HP { get; set; }
    public int Armor { get; set; }

    public Polina(string name, int hp, int armor)
    {
        Name = name;
        HP = hp;
        Armor = armor;
    }
}

public class DamageSystem
{
    private readonly IDamageCalculator _damageCalculator;

    public DamageSystem(IDamageCalculator damageCalculator)
    {
        _damageCalculator = damageCalculator;
    }

    public void ApplyDamage(Polina target, int baseDamage)
    {
        int damage = _damageCalculator.CalculateDamage(
            baseDamage,
            target.Armor
        );

        target.HP = Math.Max(0, target.HP - damage);

        Console.WriteLine(
            $"Полина {target.Name} получила {damage} урона. Осталось HP: {target.HP}"
        );
    }
}

public class Program
{
    public static void Main()
    {
        Polina polina = new Polina("Огр", 200, 20);

        int initialHP = 200;
        int baseDamage = 60;

        // Физ
        Console.WriteLine("--- Физический урон ---");

        polina.HP = initialHP;

        DamageSystem physicalSystem =
            new DamageSystem(new PhysicalDamageCalculator());

        physicalSystem.ApplyDamage(polina, baseDamage);

        // Маг
        Console.WriteLine("\n--- Магический урон ---");

        polina.HP = initialHP;

        DamageSystem magicSystem =
            new DamageSystem(new MagicDamageCalculator());

        magicSystem.ApplyDamage(polina, baseDamage);

        // Огонь
        Console.WriteLine("\n--- Огненный урон ---");

        polina.HP = initialHP;

        DamageSystem fireSystem =
            new DamageSystem(new FireDamageCalculator());

        fireSystem.ApplyDamage(polina, baseDamage);
    }
}
