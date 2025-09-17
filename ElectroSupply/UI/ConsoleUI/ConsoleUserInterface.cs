using ElectroSupply.Domain.Entities;
using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.UI.ConsoleUI;

/// <summary>
/// Консольный пользовательский интерфейс для взаимодействия с приложением через командную строку
/// </summary>
public class ConsoleUserInterface : IUserInterface
{
    /// <summary>
    /// Считывает генераторы от пользователя
    /// </summary>
    /// <returns>Предоставленные генераторы</returns>
    public IReadOnlyCollection<IGenerator> ReadGenerators()
    {
        Console.WriteLine("Введите количество генераторов: ");
        var generatorCount = ReadInt();
        var generators = new List<IGenerator>();

        for (int i = 1; i <= generatorCount; i++)
        {
            Console.WriteLine($"Генератор {i}: ");
            Console.Write($"\tНазвание: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
                name = $"Генератор {i}";
            Console.Write($"\tМощность(кВт): ");
            var power = ReadDouble();
            Console.Write("\tПотребление(л/ч): ");
            var fuel = ReadDouble();
            generators.Add(Generator.Create(name, power, fuel));
        }
        Console.WriteLine();

        return generators;
    }

    /// <summary>
    /// Считывает требуемую мощность от пользователя
    /// </summary>
    /// <returns>Требуемая мощность</returns>
    public Power ReadRequiredPower()
    {
        Console.Write("Введите требуемую мощность (кВт): ");
        var power = ReadDouble();
        return new Power(power);
    }

    /// <summary>
    /// Считывает требуемое количество дней от пользователя
    /// </summary>
    /// <returns>Количество дней</returns>
    public Days ReadRequiredDays()
    {
        Console.Write("Введите количество дней: ");
        var days = ReadInt();
        return new Days(days);
    }

    /// <summary>
    /// Выводит результат
    /// </summary>
    /// <param name="generators">Выбранные генераторы</param>
    /// <param name="totalFuel">Требуемое общее топливо</param>
    public void DisplayResult(IReadOnlyCollection<IGenerator> generators, Fuel totalFuel)
    {
        Console.WriteLine("ИТОГИ:\n");
        Console.WriteLine($"Выбранные генераторы:");
        
        foreach (var generator in generators)
        {
            Console.WriteLine($"\t- {generator.Name}, ({generator.Power} кВт, {generator.FuelConsumption} л/ч)");
        }

        Console.WriteLine($"Общее количество топлива: {totalFuel} л");
        Console.WriteLine("\nПоздравляем! Благодаря вам \"Северные интеллектуальные решения\" экономят на генераторах!");
    }

    /// <summary>
    /// Выводит сообщение об ошибке
    /// </summary>
    /// <param name="message">Текст сообщения</param>
    public void DisplayError(string message)
    {
        Console.WriteLine(message);
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

            Console.Write("Некорректный ввод. Введите целое число больше 0 (пример-> 1): ");
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

            Console.Write("Некорректный ввод. Введите число больше 0 (пример-> 3,14): ");
        }
    }
}