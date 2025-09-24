using ElectroSupply.Domain.Interfaces;

namespace ElectroSupply.Application.Interfaces;

/// <summary>
/// Общий интерфейс возвращаемого значения запроса для расчёта
/// </summary>
public interface IResult
{
    /// <summary>
    /// Коллекция генераторов
    /// </summary>
    IReadOnlyCollection<IGenerator> Generators { get; }
}