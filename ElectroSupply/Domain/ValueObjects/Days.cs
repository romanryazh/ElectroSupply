using System.Globalization;

namespace ElectroSupply.Domain.ValueObjects;

public record Days
{
    public double Value { get; init; }

    public Days(double value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Количество дней должно быть положительным", nameof(value));
        }

        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}