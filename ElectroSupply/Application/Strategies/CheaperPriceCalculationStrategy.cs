using ElectroSupply.Application.DTOs;
using ElectroSupply.Application.Interfaces;
using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Application.Strategies;

public class CheaperPriceCalculationStrategy(List<FuelType> fuelTypes) : ICalculationStrategy
{
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

    private decimal GetPriceForPowerUnit(decimal price, double fuel, double power)
    {
        return (price * (decimal)fuel) / (decimal)power;
    }
    
    
}