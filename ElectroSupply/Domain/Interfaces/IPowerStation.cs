namespace ElectroSupply.Domain.Interfaces;

/// <summary>
/// Интерфейс для Электростанции, абстрагирует агрегат
/// </summary>
public interface IPowerStation
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Коллекция генераторов на электростанции
    /// </summary>
    public IReadOnlyCollection<IGenerator> Generators { get; }
    
    /// <summary>
    /// Добавляет генератор на электростанцию
    /// </summary>
    /// <param name="generator"></param>
    public void AddGenerator(IGenerator generator);
    
    /// <summary>
    /// Добавляет коллекцию генераторов на электростанцию
    /// </summary>
    /// <param name="generators">Коллекция добавляемых генераторов</param>
    public void AddRangeGenerators(IEnumerable<IGenerator> generators);
}