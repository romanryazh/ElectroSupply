using Bogus;
using ElectroSupply.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Domain;

public class FuelTests
{
    private readonly Faker _faker = new();
    
    public FuelTests()
    {
        
    }
    
    [Fact]
    public void CreateFuel_WithValidData_CreatesValidObject()
    {
        var value = _faker.Random.Double();
        var fuelTypeName = _faker.Commerce.Product();
        
        var fuel = new Faker<Fuel>()
            .CustomInstantiator(f => new Fuel(value, fuelTypeName)).Generate();
        
        fuel.Should().NotBeNull();
        fuel.Value.Should().Be(value);
        fuel.FuelTypeName.Should().Be(fuelTypeName);
    }

    [Theory]
    [InlineData(-34)]
    [InlineData(-1)]
    public void Create_WithInvalidValue_ShouldThrow(double invalidValue)
    {
        var fuelTypeName = _faker.Commerce.Product();
        
        var action = () => new Faker<Fuel>()
            .CustomInstantiator(f => new Fuel(invalidValue, fuelTypeName)).Generate();
        
        action.Should().Throw<ArgumentException>();
    }
    
    
}