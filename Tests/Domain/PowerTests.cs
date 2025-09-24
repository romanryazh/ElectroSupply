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
    
    /// <summary>
    /// Тестирует создание экземпляра <see cref="Power"/> с валидными данными.
    /// Проверяет на не Null, сопоставляет значение свойства Value до создания экземпляра и после.
    /// </summary>
    [Fact]
    public void CreatePower_WithValidData_CreatesValidObject()
    {
        var value = _faker.Random.Double(min: 0);
        
        var power = new Faker<Power>()
            .CustomInstantiator(f => new Power(value)).Generate();
        
        power.Should().NotBeNull();
        power.Value.Should().Be(value);
    }

    /// <summary>
    /// Тестирует создание экземпляра <see cref="Power"/> с некорректным значением Value. Проверяет выбрасывание исключения. 
    /// </summary>
    /// <param name="invalidValue">Некорректное значение Value</param>
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