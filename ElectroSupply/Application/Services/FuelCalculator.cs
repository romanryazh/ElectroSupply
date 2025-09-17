using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Application.Services;

/// <summary>
/// Сервис для расчёта итогового потребления топлива работающими генераторами
/// </summary>
public class FuelCalculator : IFuelCalculator
{
    private readonly IFuelCalculationStrategy _strategy;

    /// <summary>
    /// Создаёт экземпляр <see cref="FuelCalculator"/>
    /// </summary>
    /// <param name="strategy">Стратегия для расчёта. Устанавливает алгоритм выбора генераторов</param>
    public FuelCalculator(IFuelCalculationStrategy strategy)
    {
        ArgumentNullException.ThrowIfNull(strategy);
            
        _strategy = strategy;
    }

    /// <summary>
    /// Вычисляет итоговое количество топлива для выбранных генераторов на указанный промежуток времени
    /// </summary>
    /// <param name="requiredPower">Необходимая мощность</param>
    /// <param name="days">Необходимое количество дней</param>
    /// <param name="generators">Коллекция предоставленных генераторов</param>
    /// <returns>Коллекция генераторов и итоговое количество топлива</returns>
    public (IReadOnlyCollection<IGenerator>, Fuel totalFuel) CalculateRequiredFuel
        (Power requiredPower, Days days, IReadOnlyCollection<IGenerator> generators)
    {
        return _strategy.Calculate(requiredPower, days, generators);
    }
}