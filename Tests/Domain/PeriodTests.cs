using Bogus;
using ElectroSupply.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Domain;

public class PeriodTests
{
    private readonly Faker _faker = new();
    
    public PeriodTests()
    {
        
    }
    
    [Fact]
    public void CreatePeriod_WithValidData_CreatesValidObject()
    {
        var value = _faker.Random.Int(min: 0);
        
        var period = new Faker<Period>()
            .CustomInstantiator(f => new Period(value)).Generate();
        
        period.Should().NotBeNull();
        period.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CreatePeriod_WithInvalidValue_ShouldThrow(int invalidValue)
    {
        var action = () => new Faker<Period>()
            .CustomInstantiator(f => new Period(invalidValue)).Generate();
        
        action.Should().Throw<ArgumentException>();
    }
}