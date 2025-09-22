using ElectroSupply.Domain.Interfaces;
using ArgumentException = System.ArgumentException;

namespace ElectroSupply.Domain.Entities;

/// <summary>
/// Электростанция для вырабатывания электричества
/// </summary>
public class PowerStation : IPowerStation
{
    private List<IGenerator> _generators;
    
    /// <summary>
    /// Название электростанции
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Коллекция генераторов на станции
    /// </summary>
    public IReadOnlyCollection<IGenerator> Generators => _generators.AsReadOnly();

    private PowerStation(string name)
    {
        Name = name;
        _generators = [];
    }

    /// <summary>
    /// Фабричный метод, создаёт новый экземпляр <see cref="PowerStation"/>
    /// </summary>
    /// <param name="name">Название</param>
    /// <returns>Новый экземпляр <see cref="PowerStation"/></returns>
    /// <exception cref="ArgumentException">
    /// Выбрасывается если <paramref name="name"/> пустое или null
    /// </exception>
    public static PowerStation Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Значение не может быть null или пустым.", nameof(name));
        }
        return new PowerStation(name);
    }

    /// <summary>
    /// Добавляет генератор на электростанцию
    /// </summary>
    /// <param name="generator"></param>
    public void AddGenerator(IGenerator generator)
    {
        if (generator == null)
        {
            throw new ArgumentNullException(nameof(generator), "Не может быть null");
        }
        
        if (_generators.Any(g => g.Name == generator.Name))
        {
            throw new ArgumentException($"Генератор с названием {generator.Name} уже добавлен");
        }
        
        _generators.Add(generator);
    }

    /// <summary>
    /// Добавляет коллекцию генераторов на электростанцию
    /// </summary>
    /// <param name="generators">Коллекция добавляемых генераторов</param>
    public void AddRangeGenerators(IEnumerable<IGenerator> generators)
    {
        foreach (var generator in generators)
        {
            if (_generators.Any(g => g.Name == generator.Name))
            {
                throw new ArgumentException($"Генератор с названием {generator.Name} уже добавлен");
            }
            _generators.Add(generator);
        }
    }
}