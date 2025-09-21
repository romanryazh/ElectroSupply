namespace ElectroSupply.Domain.ValueObjects;

public record FuelType
{
    public string Name { get; init; }
    
    public decimal Price { get; init; }

    public FuelType(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price));
        }
        
        Name = name;
        Price = price;
    }
}