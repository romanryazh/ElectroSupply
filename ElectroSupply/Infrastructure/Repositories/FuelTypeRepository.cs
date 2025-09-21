using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Infrastructure.Repositories;

public class FuelTypeRepository : IFuelTypeRepository
{
    private readonly List<FuelType> _generators =
    [
        new FuelType("Бензин", 10),
        new FuelType("Слёзы джунов", 5),
        new FuelType("Дизель", 15)
    ];
    
    public List<FuelType> GetFuelTypes()
    {
        return _generators;
    }

    public void AddFuelType(FuelType fuelType)
    {
        _generators.Add(fuelType);
    }

    public FuelType GetFuelTypeByName(string name)
    {
        var fuelType = _generators.FirstOrDefault(x => x.Name == name);

        if (fuelType == null)
        {
            throw new NullReferenceException($"Тип топлива с названием {name} не найден");
        }

        return fuelType;
    }

    public bool FuelTypeExists(string name)
    {
        return _generators.Any(x => x.Name == name);
    }
}