using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Domain.Interfaces;

public interface IFuelTypeRepository
{
    public List<FuelType> GetFuelTypes();
    
    public void AddFuelType(FuelType fuelType);

    public bool FuelTypeExists(string name);
}