using ElectroSupply.Application.DTOs;
using ElectroSupply.Application.Interfaces;
using ElectroSupply.Domain.Entities;
using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Application.Strategies;

/// <summary>
/// Стратегия расчёта потребляемого топлива с выборкой генераторов по их эффективности
/// (используя в первую очередь самые эффективные)
/// </summary>
public class GreaterEfficiencyFuelCalculationStrategy : ICalculationStrategy
{
    /// <summary>
    /// Вычисляет итоговое количество топлива для выбранных генераторов на указанный промежуток времени 
    /// </summary>
    /// <param name="requiredPower">Необходимая мощность</param>
    /// <param name="period">Необходимое количество дней</param>
    /// <param name="generators">Коллекция предоставленных генераторов</param>
    /// <returns>Коллекция генераторов и итоговое количество топлива</returns>
    /// <exception cref="InvalidOperationException">
    /// Выбрасывается если мощности предоставленных генераторов недостаточно для задачи 
    /// </exception>
    public IResult Calculate
        (Power requiredPower, Period period, IReadOnlyCollection<IGenerator> generators)
    {

        var sortedGenerators = generators
            .OrderByDescending(g => g.Efficiency).ToList();
        var usedGenerators = new List<IGenerator>();
        double power = 0;
        double fuel = 0;
        
        foreach (var generator in sortedGenerators)
        {
            if (power >= requiredPower.Value) break;
        
            power += generator.Power.Value;
            usedGenerators.Add(generator);
            fuel += generator.FuelConsumption.Value * 24 * period.Value;
        }
        
        if (power < requiredPower.Value)
        {
            throw new InvalidOperationException(
                "Не удалось выполнить операцию - недостаточно мощности предоставленных генераторов");
        }
        
        return new EfficientFuelResult(usedGenerators.AsReadOnly(), fuel);
    }

}