using System.Globalization;

namespace ElectroSupply.Domain.ValueObjects;

/// <summary>
/// Мощность
/// </summary>
public record Power
{
    /// <summary>
    /// Значение для операций
    /// </summary>
    public double Value { get; init; }

    /// <summary>
    /// Конструктор записи
    /// </summary>
    /// <param name="value">значение</param>
    /// <exception cref="ArgumentException">Выбрасывается если <paramref name="value"/> меньше 0</exception>
    public Power(double value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Мощность не может быть отрицательной", nameof(value));
        }

        Value = value;
    }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}