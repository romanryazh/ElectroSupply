using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Application.Services;

public class FuelCalculator : IFuelCalculator
{
    private readonly IFuelCalculationStrategy _strategy;

    public FuelCalculator(IFuelCalculationStrategy strategy)
    {
        ArgumentNullException.ThrowIfNull(strategy);
            
        _strategy = strategy;
    }
    
    public (IReadOnlyCollection<IGenerator>, Fuel totalFuel) CalculateRequiredFuel
        (Power requiredPower, Days days, IReadOnlyCollection<IGenerator> generators)
    {
        return _strategy.Calculate(requiredPower, days, generators);
    }
}