using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Domain.Entities;

/// <summary>
/// Электрогенератор 
/// </summary>
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
    /// Фабричный метод, создаёт новый экземпляр <see cref="Generator"/>
    /// </summary>
    /// <param name="name">Название</param>
    /// <param name="power">Выходная мощность</param>
    /// <param name="fuelConsumption">Потребляемая мощность</param>
    /// <returns>Новый экземпляр <see cref="Generator"/></returns>
    /// <exception cref="ArgumentException">
    /// Выбрасывается если:
    /// <para><paramref name="name"/> пустое или null</para>
    /// <para><paramref name="power"/> меньше или равна 0</para>
    /// <para><paramref name="fuelConsumption"/> меньше или равно 0</para>
    /// </exception>
    public static Generator Create(string name, double power, double fuelConsumption)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Значение не может быть null или пустым.", nameof(name));
        }

        if (power <= 0)
        {
            throw new ArgumentException("Мощность должна быть больше нуля", nameof(power));
        }

        if (fuelConsumption <= 0)
        {
            throw new ArgumentException("Расход топлива должен быть больше нуля", nameof(fuelConsumption));
        }
        
        return new Generator(name, new Power(power), new Fuel(fuelConsumption));
    }
    
    
}