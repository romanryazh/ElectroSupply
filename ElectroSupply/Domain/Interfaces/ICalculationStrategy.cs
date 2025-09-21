using ElectroSupply.Application.DTOs;
using ElectroSupply.Application.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Domain.Interfaces;

/// <summary>
///  Стратегия для расчёта. Устанавливает алгоритм выбора генераторов
/// </summary>
public interface ICalculationStrategy
{
    /// <summary>
    /// Вычисляет итоговое количество топлива для выбранных генераторов на указанный промежуток времени 
    /// </summary>
    /// <param name="requiredPower">Необходимая мощность</param>
    /// <param name="period">Необходимое количество дней</param>
    /// <param name="generators">Коллекция предоставленных генераторов</param>
    /// <returns>Коллекция генераторов и итоговое количество топлива</returns>
    public IResult Calculate(Power requiredPower, Period period, IReadOnlyCollection<IGenerator> generators);
}