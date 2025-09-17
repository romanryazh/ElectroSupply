using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Domain.Interfaces;

/// <summary>
///  Стратегия для расчёта. Устанавливает алгоритм выбора генераторов
/// </summary>
public interface IFuelCalculationStrategy
{
    /// <summary>
    /// Вычисляет итоговое количество топлива для выбранных генераторов на указанный промежуток времени 
    /// </summary>
    /// <param name="requiredPower">Необходимая мощность</param>
    /// <param name="days">Необходимое количество дней</param>
    /// <param name="generators">Коллекция предоставленных генераторов</param>
    /// <returns>Коллекция генераторов и итоговое количество топлива</returns>
    public (IReadOnlyCollection<IGenerator>, Fuel totalFuel) Calculate
        (Power requiredPower, Days days, IReadOnlyCollection<IGenerator> generators);
}