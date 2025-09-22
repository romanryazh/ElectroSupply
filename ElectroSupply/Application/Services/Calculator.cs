using ElectroSupply.Application.Interfaces;
using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Application.Services;

/// <summary>
/// Сервис для расчёта итогового потребления топлива работающими генераторами
/// </summary>
public class Calculator : ICalculator
{
    private ICalculationStrategy _strategy;

    public Calculator()
    {
        
    }
    
    /// <summary>
    /// Создаёт экземпляр <see cref="Calculator"/>
    /// </summary>
    /// <param name="strategy">Стратегия для расчёта. Устанавливает алгоритм выбора генераторов</param>
    public Calculator(ICalculationStrategy strategy)
    {
        ArgumentNullException.ThrowIfNull(strategy);
            
        _strategy = strategy;
    }

    public void SetStrategy(ICalculationStrategy strategy)
    {
        ArgumentNullException.ThrowIfNull(strategy);
        
        _strategy = strategy;
    }

    /// <summary>
    /// Вычисляет итоговое количество топлива для выбранных генераторов на указанный промежуток времени
    /// </summary>
    /// <param name="requiredPower">Необходимая мощность</param>
    /// <param name="period">Необходимое количество дней</param>
    /// <param name="generators">Коллекция предоставленных генераторов</param>
    /// <returns>Коллекция генераторов и итоговое количество топлива</returns>
    public IResult Calculate
        (Power requiredPower, Period period, IReadOnlyCollection<IGenerator> generators)
    {
        return _strategy.Calculate(requiredPower, period, generators);
    }
}