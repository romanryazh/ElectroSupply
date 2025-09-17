using ElectroSupply.Domain.Entities;
using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.UI.ConsoleUI;

public class ConsoleUserInterface : IUserInterface
{
    public IReadOnlyCollection<IGenerator> ReadGenerators()
    {
        Console.WriteLine("Введите количество генераторов: ");
        var generatorCount = ReadInt();
        var generators = new List<IGenerator>();

        for (int i = 1; i <= generatorCount; i++)
        {
            Console.WriteLine($"Генератор {i}: ");
            Console.Write($"\tНазвание: ");
            var name = Console.ReadLine() ?? $"Генератор {i}";
            Console.WriteLine($"\tМощность(кВт): ");
            var power = ReadDouble();
            Console.WriteLine("\tПотребление(л/ч): ");
            var fuel = ReadDouble();
            generators.Add(Generator.Create(name, power, fuel));
        }

        return generators;
    }

    public Power ReadRequiredPower()
    {
        Console.WriteLine("Введите требуемую мощность (кВт): ");
        var power = ReadDouble();
        return new Power(power);
    }

    public Days ReadRequiredDays()
    {
        Console.WriteLine("Введите количество дней: ");
        var days = ReadInt();
        return new Days(days);
    }

    public void DisplayResult(IReadOnlyCollection<IGenerator> generators, Fuel totalFuel)
    {
        Console.WriteLine($"Выбранные генераторы:");
        
        foreach (var generator in generators)
        {
            Console.WriteLine($"\t{generator.Name}, ({generator.Power} кВт, {generator.FuelConsumption} л/ч)");
        }

        Console.WriteLine($"Общее количество топлива: {totalFuel} л");
    }

    private int ReadInt()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var value) && value > 0)
            {
                return value;
            }

            Console.WriteLine("Некорректный ввод. Введите целое число больше 0");
        }
    }

    private double ReadDouble()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (double.TryParse(input, out var value) && value > 0)
            {
                return value;
            }

            Console.WriteLine("Некорректный ввод. Введите число больше 0");
        }
    }
}