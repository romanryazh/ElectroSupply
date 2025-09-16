using ElectroSupply.Domain.Interfaces;

namespace ElectroSupply.Domain.Entities;

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
    /// Фабричный метод создания нового экземпляра <see cref="PowerStation"/>
    /// </summary>
    /// <param name="name">Название</param>
    /// <returns>Новый экземпляр <see cref="PowerStation"/></returns>
    public static PowerStation Create(string name)
    {
        return new PowerStation(name);
    }
    
    /// <summary>
    /// Добавить генератор
    /// </summary>
    /// <param name="generator">Добавляемый генератор</param>
    public void AddGenerator(IGenerator generator)
    {
        _generators.Add(generator);
    }
}