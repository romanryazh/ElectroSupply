using Bogus;
using ElectroSupply.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Domain;

/// <summary>
/// Unit-тесты для объекта <see cref="Period"/>
/// </summary>
public class PeriodTests
{
    private readonly Faker _faker = new();
    
    public PeriodTests()
    {
        
    }
    /// <summary>
    /// Тестирует создание экземпляра <see cref="Period"/> с валидными данными. Проверяет на не Null, сопоставляет значения свойства до создания экземпляра и после.
    /// </summary>
    [Fact]
    public void CreatePeriod_WithValidData_CreatesValidObject()
    {
        var value = _faker.Random.Int(min: 0);
        
        var period = new Faker<Period>()
            .CustomInstantiator(f => new Period(value)).Generate();
        
        period.Should().NotBeNull();
        period.Value.Should().Be(value);
    }

    /// <summary>
    /// Тестирует создание экземпляра <see cref="Period"/> с некорректным значением Value. Проверяет выбрасывание исключения.
    /// </summary>
    /// <param name="invalidValue">Некорректное значение для Value</param>
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