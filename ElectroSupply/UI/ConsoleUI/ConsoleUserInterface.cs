using ElectroSupply.Application.DTOs;
using ElectroSupply.Application.Interfaces;
using ElectroSupply.Domain.Entities;
using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.UI.ConsoleUI;

/// <summary>
/// Консольный пользовательский интерфейс для взаимодействия с приложением через командную строку
/// </summary>
public class ConsoleUserInterface(IFuelTypeRepository fuelTypeRepository) : IUserInterface
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
            
            DisplayFuelTypes();
            while (true)
            {
                Console.Write("\tТип топлива(название):");
                var fuelTypeName = Console.ReadLine();
                if (fuelTypeName != null && fuelTypeRepository.FuelTypeExists(fuelTypeName))
                {
                    generators.Add(Generator.Create(name, power, fuel, fuelTypeName));
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректное название типа топлива");
                }
            }
            
        }
        
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
    public Period ReadRequiredDays()
    {
        Console.Write("Введите количество дней: ");
        var days = ReadInt();
        return new Period(days);
    }

    public FuelType ReadFuelType()
    {
        Console.Write($"\tНазвание типа топлива: ");
        var name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
            name = $"Название";
        
        Console.Write($"\tСтоимость(за 1 литр): ");
        var price = (decimal)ReadDouble();
        
        return new FuelType(name, price);
    }

    public int ReadOperation()
    {
        Console.WriteLine("1 - Добавить новый тип топлива");
        Console.WriteLine("2 - Рассчитать наиболее эффективное энергообеспечение");
        Console.WriteLine("3 - Рассчитать наиболее дешёвое энергообеспечение");
        Console.WriteLine("4 - Выход");
        Console.Write("Выберите нужную операцию(номер): ");
        return ReadIntRange(1, 4);

    }

    /// <summary>
    /// Выводит результат
    /// </summary>
    /// <param name="generators">Выбранные генераторы</param>
    /// <param name="totalFuel">Требуемое общее топливо</param>
    public void DisplayResult(IResult result)
    {
        Console.WriteLine("ИТОГИ:\n");
        Console.WriteLine($"Выбранные генераторы:");
        
        foreach (var generator in result.Generators)
        {
            Console.WriteLine($"\t- {generator.Name}, ({generator.Power} кВт, {generator.FuelConsumption} л/ч)");
        }

        switch (result)
        {
            case EfficientFuelResult efficientFuelResult:
                Console.WriteLine($"Общее количество топлива: {efficientFuelResult.TotalFuel} л");
                Console.WriteLine("\nПоздравляем! Благодаря вам \"Северные интеллектуальные решения\" экономят на генераторах!");
                break;
            case CheaperPriceResult cheaperPriceResult:
                Console.WriteLine($"Самая дешёвая стоимость: {cheaperPriceResult.TotalPrice}");
                break;
        }
    }

    /// <summary>
    /// Выводит сообщение об ошибке
    /// </summary>
    /// <param name="message">Текст сообщения</param>
    public void DisplayError(string message)
    {
        Console.WriteLine(message);
    }

    public void DisplayFuelTypes()
    {
        Console.WriteLine("Типы топлива:");
        
        var fueltypes = fuelTypeRepository.GetFuelTypes();
        
        foreach (var fuelType in fueltypes)
        {
            Console.WriteLine($"\t- {fuelType.Name}");
        }
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

    private int ReadIntRange(int minValue = 1, int maxValue = 1)
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var value) && value >= minValue && value <= maxValue)
            {
                return value;
            }

            Console.Write($"Некорректный ввод. Введите целое число от {minValue} до {maxValue}(пример-> 1): ");
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