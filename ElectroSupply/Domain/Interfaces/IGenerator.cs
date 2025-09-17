using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.Domain.Interfaces;

/// <summary>
/// Интерфейс для электрогенератора
/// </summary>
public interface IGenerator
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
    public double Efficiency { get; }
}