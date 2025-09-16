using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Domain.Interfaces;

public interface IGenerator
{
    public string Name { get; }
    
    public Power Power { get; }
    
    public Fuel FuelConsumption { get; }
    
    public double Efficiency { get; }
}