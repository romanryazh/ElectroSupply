using Bogus;
using ElectroSupply.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Domain;

public class PowerTests
{
    private readonly Faker _faker = new();
    
    public PowerTests()
    {
        
    }
    
    [Fact]
    public void CreatePower_WithValidData_CreatesValidObject()
    {
        var value = _faker.Random.Double(min: 0);
        
        var power = new Faker<Power>()
            .CustomInstantiator(f => new Power(value)).Generate();
        
        power.Should().NotBeNull();
        power.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(-20)]
    [InlineData(-1)]
    public void CreatePower_WithInvalidValue_ShouldThrow(double invalidValue)
    {
        var action = () => new Faker<Power>()
            .CustomInstantiator(f => new Power(invalidValue)).Generate();
        
        action.Should().Throw<ArgumentException>();
    }
}