using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Domain.Interfaces;

public interface IFuelCalculator
{
    public (IReadOnlyCollection<IGenerator>, Fuel totalFuel) CalculateRequiredFuel
        (Power requiredPower, Days days, IReadOnlyCollection<IGenerator> generators);
}