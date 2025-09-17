using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Domain.Interfaces;

public interface IFuelCalculator
{
    public (IReadOnlyCollection<IGenerator>, Fuel totalFuel) CalculateRequiredFuel
        (double requiredPower, int days, IReadOnlyCollection<IGenerator> generators);
}