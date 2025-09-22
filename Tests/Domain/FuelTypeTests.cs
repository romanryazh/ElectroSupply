using Bogus;
using ElectroSupply.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Domain;

public class FuelTypeTests
{
    private readonly Faker _faker = new();
    
    public FuelTypeTests()
    {
        
    }
    
    [Fact]
    public void CreateFuelType_WithValidData_CreatesValidObject()
    {
        var name = _faker.Commerce.Product();
        var price = 10;
        
        var fuelType = new Faker<FuelType>()
            .CustomInstantiator(f => new FuelType(name, price)).Generate();
        
        fuelType.Should().NotBeNull();
        fuelType.Name.Should().Be(name);
        fuelType.Price.Should().Be(price);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void CreateFuelType_WithInvalidName_ShouldThrow(string invalidName)
    {
        var action = () => new Faker<FuelType>()
            .CustomInstantiator(f => new FuelType(invalidName, 10)).Generate();
        
        action.Should().Throw<ArgumentNullException>();
    }
    
    [Theory]
    [InlineData(-1)]
    public void CreateFuelType_WithInvalidPrice_ShouldThrow(decimal invalidPrice)
    {
        var name = _faker.Commerce.Product();
        
        var action = () => new Faker<FuelType>()
            .CustomInstantiator(f => new FuelType(name, invalidPrice)).Generate();
        
        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}