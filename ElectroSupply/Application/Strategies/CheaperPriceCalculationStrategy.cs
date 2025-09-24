using ElectroSupply.Application.DTOs;
using ElectroSupply.Application.Interfaces;
using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Application.Strategies;

/// <summary>
/// Стратегия расчёта наиболее дешёвого энергопотребления 
/// </summary>
/// <param name="fuelTypes">Типы топлива</param>
public class CheaperPriceCalculationStrategy(List<FuelType> fuelTypes) : ICalculationStrategy
{
    /// <summary>
    /// Вычисляет итоговую стоимость
    /// </summary>
    /// <param name="requiredPower">Необходимая мощность</param>
    /// <param name="period">Необходимое количество дней</param>
    /// <param name="generators">Коллекция предоставленных генераторов</param>
    /// <returns>Коллекция генераторов и итоговая наиболее низкая стоимость</returns>
    /// <exception cref="InvalidOperationException">
    /// Выбрасывается если мощности предоставленных генераторов недостаточно для задачи 
    /// </exception>
    public IResult Calculate(Power requiredPower, Period period, IReadOnlyCollection<IGenerator> generators)
    {
        var sortedGenerators = generators
        .Select(g => new
        {
            Generator = g,
            PricePerPowerUnit = GetPriceForPowerUnit(
                fuelTypes.FirstOrDefault(f => f.Name == g.FuelConsumption.FuelTypeName)!.Price,
                g.FuelConsumption.Value, 
                g.Power.Value)
        })
        .OrderBy(s => s.PricePerPowerUnit);
        
        var usedGenerators = new List<IGenerator>();
        double power = 0;
        decimal totalPrice = 0;
        
        foreach (var item in sortedGenerators)
        {
            if (power >= requiredPower.Value) break;
            
            power += item.Generator.Power.Value;
            usedGenerators.Add(item.Generator);
            totalPrice += item.PricePerPowerUnit * (decimal)item.Generator.Power.Value * 24 * (decimal)period.Value;
        }
        
        if (power < requiredPower.Value)
        {
            throw new InvalidOperationException(
                "Не удалось выполнить операцию - недостаточно мощности предоставленных генераторов");
        }
        
        return new CheaperPriceResult(usedGenerators, totalPrice);
    }

    /// <summary>
    /// Вычисляет стоимость за одну единицу энергии (кВт)
    /// </summary>
    /// <param name="price">Стоимость топлива</param>
    /// <param name="fuel">Количество топлива</param>
    /// <param name="power">Мощность генератора</param>
    /// <returns></returns>
    private decimal GetPriceForPowerUnit(decimal price, double fuel, double power)
    {
        return (price * (decimal)fuel) / (decimal)power;
    }
    
    
}