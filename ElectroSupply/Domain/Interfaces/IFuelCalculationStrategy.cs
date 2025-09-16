using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Domain.Interfaces;

public interface IFuelCalculationStrategy
{
    public (IReadOnlyCollection<IGenerator>, Fuel totalFuel) Calculate
        (Power requiredPower, Days days, IReadOnlyCollection<IGenerator> generators);
}