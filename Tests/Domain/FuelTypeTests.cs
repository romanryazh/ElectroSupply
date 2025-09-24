using Bogus;
using ElectroSupply.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Domain;

/// <summary>
/// Unit-тесты для объекта <see cref="FuelType"/>
/// </summary>
public class FuelTypeTests
{
    private readonly Faker _faker = new();
    
    public FuelTypeTests()
    {
        
    }
    /// <summary>
    /// Тестирует создание экземпляра <see cref="FuelType"/> с валидными данными. Проверяет на не Null, сопоставляет значения свойств до создания экземпляра и после.
    /// </summary>
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
    
    /// <summary>
    /// Тестирует создание экземпляра FuelType с невалидным Name. Проверяет выбрасывание исключения при создании <see cref="FuelType"/> 
    /// </summary>
    /// <param name="invalidName">Некорректное значение для Name</param>
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
    
    /// <summary>
    /// Тестирует создание экземпляра FuelType с невалидным Price. Проверяет выбрасывание исключения при создании <see cref="FuelType"/>
    /// </summary>
    /// <param name="invalidPrice">Некорректное значение для Price</param>
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