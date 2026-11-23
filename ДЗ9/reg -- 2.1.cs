using System;

public interface ICombatant
{
    string Name { get; }
    int HP { get; set; }
    bool IsDead { get; }
    int Damage { get; }

    void Attack(ICombatant target);
    void TakeDamage(int amount);
}

public class Knight : ICombatant
{
    public string Name { get; }
    public int HP { get; set; }
    public bool IsDead => HP <= 0;
    public int Damage { get; }

    public Knight(string name)
    {
        Name = name;
        HP = 150;
        Damage = 25;
    }

    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine(
                $"Рыцарь {Name} не может атаковать, потому что погиб."
            );
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine(
                $"Рыцарь {Name} не может атаковать мёртвую цель."
            );
            return;
        }

        Console.WriteLine($"Рыцарь {Name} атакует!");

        target.TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);

        Console.WriteLine(
            $"Рыцарь {Name} получил {amount} урона. Осталось HP: {HP}"
        );
    }
}

public class Mage : ICombatant
{
    public string Name { get; }
    public int HP { get; set; }
    public bool IsDead => HP <= 0;
    public int Damage { get; }

    public Mage(string name)
    {
        Name = name;
        HP = 80;
        Damage = 40;
    }

    public void Attack(ICombatant target)
    {
        if (target.IsDead)
        {
            Console.WriteLine(
                $"Маг {Name} не может атаковать мёртвую цель"
            );
            return;
        }

        Console.WriteLine($"Маг {Name} использует двойное заклинание!");

        
        target.TakeDamage(Damage);

        
        if (!target.IsDead)
        {
            target.TakeDamage(Damage);
        }
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);

        Console.WriteLine(
            $"Маг {Name} получил {amount} урона. Осталось HP: {HP}"
        );
    }
}

public class Program
{
    public static void Main()
    {
        ICombatant knight = new Knight("Полина");
        ICombatant mage = new Mage("Домашка");

        while (!knight.IsDead && !mage.IsDead)
        {
            knight.Attack(mage);

            if (!mage.IsDead && !knight.IsDead)
            {
                mage.Attack(knight);
            }
        }

        if (knight.IsDead)
        {
            Console.WriteLine($"Победил Маг {mage.Name}!");
        }
        else
        {
            Console.WriteLine($"Победил Рыцарь {knight.Name}!");
        }
    }
}
