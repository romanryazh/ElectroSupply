using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Domain.Entities;

public class Generator : IGenerator
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Выходная мощность (кВт)
    /// </summary>
    public Power Power { get; }
    
    /// <summary>
    /// Количество потребляемого топлива (л/ч)
    /// </summary>
    public Fuel FuelConsumption { get; }
    
    /// <summary>
    /// Эффективность генератора (мощность/потребление)
    /// </summary>
    public double Efficiency => Power.Value / FuelConsumption.Value;

    private Generator(string name, Power power, Fuel fuelConsumption)
    {
        Name = name;
        Power = power;
        FuelConsumption = fuelConsumption;
    }

    /// <summary>
    /// Фабричный метод для создания экземпляра <see cref="Generator"/>
    /// </summary>
    /// <param name="name">Название</param>
    /// <param name="power">Выходная мощность</param>
    /// <param name="fuelConsumption">Потребляемая мощность</param>
    /// <returns>Новый экземпляр <see cref="Generator"/></returns>
    public static Generator Create(string name, double power, double fuelConsumption)
    {
        return new Generator(name, new Power(power), new Fuel(fuelConsumption));
    }
    
    
}