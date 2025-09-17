using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Application.Strategies;

public class GreaterEfficiencyFuelCalculationStrategy : IFuelCalculationStrategy
{
    public (IReadOnlyCollection<IGenerator>, Fuel totalFuel) Calculate
        (Power requiredPower, Days days, IReadOnlyCollection<IGenerator> generators)
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
            fuel += generator.FuelConsumption.Value * 24 * days.Value;
        }

        if (power < requiredPower.Value)
        {
            throw new InvalidOperationException(
                "Не удалось выполнить операцию - недостаточно мощности предоставленных генераторов");
        }
        
        return (usedGenerators.AsReadOnly(), new Fuel(fuel));
    }
    
}