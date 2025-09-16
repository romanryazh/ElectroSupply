namespace ElectroSupply.Domain.ValueObjects;

public record Fuel
{
    public double Value { get; init; }

    public Fuel(double value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Топливо не может быть отрицательным", nameof(value));
        }

        Value = value;
    }
}