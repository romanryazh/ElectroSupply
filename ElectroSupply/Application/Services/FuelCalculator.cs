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
        (double requiredPower, int days, IReadOnlyCollection<IGenerator> generators)
    {
        return _strategy.Calculate(new Power(requiredPower), new Days(days), generators);
    }
}