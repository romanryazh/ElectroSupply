using System.Globalization;

namespace ElectroSupply.Domain.ValueObjects;

/// <summary>
/// Топливо 
/// </summary>
public record Fuel
{
    /// <summary>
    /// Значение для операций
    /// </summary>
    public double Value { get; init; }
    
    public string FuelTypeName { get; init; }
    
    /// <summary>
    /// Конструктор записи
    /// </summary>
    /// <param name="value">значение</param>
    /// <exception cref="ArgumentException">Выбрасывается если <paramref name="value"/> меньше 0</exception>
    public Fuel(double value, string fuelTypeName)
    {
        if (value < 0)
        {
            throw new ArgumentException("Топливо не может быть отрицательным", nameof(value));
        }

        Value = value;
        FuelTypeName = fuelTypeName;
    }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}