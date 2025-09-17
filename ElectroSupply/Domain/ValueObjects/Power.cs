using System.Globalization;

namespace ElectroSupply.Domain.ValueObjects;

public record Power
{
    public double Value { get; init; }

    public Power(double value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Мощность не может быть отрицательной", nameof(value));
        }

        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}