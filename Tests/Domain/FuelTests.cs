using Bogus;
using ElectroSupply.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Domain;

/// <summary>
/// Unit-тесты для объекта <see cref="Fuel"/>
/// </summary>
public class FuelTests
{
    private readonly Faker _faker = new();
    
    public FuelTests()
    {
        
    }
    
    /// <summary>
    /// Тестирует создание экземпляра <see cref="Fuel"/> с валидными данными. Проверяет на не Null, корректность значения и имя типа топлива.
    /// </summary>
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

    /// <summary>
    /// Тестирует создание экземпляра <see cref="Fuel"/> с некорректным значением Value. Проверяет корректность выбрасывания исключения при создании Fuel
    /// </summary>
    /// <param name="invalidValue">Некорректное значение для Value у <see cref="Fuel"/></param>
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