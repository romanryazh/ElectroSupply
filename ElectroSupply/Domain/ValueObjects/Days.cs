using System.Globalization;

namespace ElectroSupply.Domain.ValueObjects;

/// <summary>
/// Количество дней
/// </summary>
public record Days
{
    /// <summary>
    /// Значение для операций
    /// </summary>
    public double Value { get; init; }

    /// <summary>
    /// Конструктор записи
    /// </summary>
    /// <param name="value">значение</param>
    /// <exception cref="ArgumentException">Выбрасывается если <paramref name="value"/> меньше или равно 0</exception>
    public Days(double value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Количество дней должно быть положительным", nameof(value));
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