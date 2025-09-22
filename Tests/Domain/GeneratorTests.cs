using Bogus;
using ElectroSupply.Domain.Entities;
using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;
using FluentAssertions;

namespace Tests.Domain;

public class GeneratorTests
{
    private readonly Faker _faker = new();
    private readonly Faker<Power> _powerFaker;
    private readonly Faker<Fuel> _fuelFaker;
    private readonly Faker<FuelType> _fuelTypeFaker;
    private readonly Faker<Period> _periodFaker;
    private readonly Faker<Generator> _generatorFaker;

    public GeneratorTests()
    {
        _fuelTypeFaker = new Faker<FuelType>()
            .CustomInstantiator(f => new FuelType(f.Commerce.Product(), f.Random.Decimal(max: 10)));
        _powerFaker = new Faker<Power>()
            .CustomInstantiator(f => new Power(f.Random.Double(max: 100)));
        _fuelFaker = new Faker<Fuel>()
            .CustomInstantiator(f => new Fuel(f.Random.Double(max: 100), f.Commerce.Product()));
        _periodFaker = new Faker<Period>()
            .CustomInstantiator(f => new Period(f.Random.Int(max: 100)));
        _generatorFaker = new Faker<Generator>()
            .CustomInstantiator(f => Generator.Create(
                _faker.Commerce.Product(),
                _powerFaker.Generate().Value,
                _fuelFaker.Generate().Value,
                _faker.Commerce.Product()));
    }

    [Fact]
    public void Create_WithValidData_CreatesValidEntity()
    {
        var fuelType = _fuelTypeFaker.Generate();
        var power = _powerFaker.Generate();
        var fuel = _fuelFaker.Generate();
        var name = _faker.Random.String(10);

        var generator = Generator.Create(name, power.Value, fuel.Value, fuelType.Name);

        generator.Should().NotBeNull();
        generator.Name.Should().Be(name);
        generator.Power.Value.Should().Be(power.Value);
        generator.FuelConsumption.Value.Should().Be(fuel.Value);
        generator.Efficiency.Should().Be(power.Value / fuel.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_WithInvalidName_ShouldThrowsException(string invalidName)
    {
        var generatorFaker = GetCustomGeneratorFaker(nameGen: () => invalidName);
        
        var action = () => generatorFaker.Generate();

        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-4)]
    public void Create_WithInvalidPower_ShouldThrowsException(double invalidPower)
    {
        var generatorFaker = GetCustomGeneratorFaker(powerGen: () => invalidPower);
        
        var action = () => generatorFaker.Generate();
        
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-4)]
    public void Create_WithInvalidFuel_ShouldThrowsException(double invalidFuel)
    {
        var generatorFaker = GetCustomGeneratorFaker(fuelGen: () => invalidFuel);
        
        var action = () => generatorFaker.Generate();
        
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_WithInvalidFuelTypeName_ShouldThrowsException(string invalidFuelType)
    {
        var generatorFaker = GetCustomGeneratorFaker(fuelTypeNameGen: () => invalidFuelType);
        
        var action = () => generatorFaker.Generate();
        
        action.Should().Throw<ArgumentException>();
    }


    private Faker<Generator> GetCustomGeneratorFaker(Func<string>? nameGen = null, 
        Func<double>? powerGen = null, Func<double>? fuelGen = null, Func<string>? fuelTypeNameGen = null)
    {
        var name = nameGen != null ? nameGen() : _faker.Commerce.Product();
        var power = powerGen != null ? powerGen() : _faker.Random.Double();
        var fuel = fuelGen != null ? fuelGen() : _fuelFaker.Generate().Value;
        var fuelTypeName = fuelTypeNameGen != null ? fuelTypeNameGen() : _faker.Commerce.Product();

        return new Faker<Generator>()
            .CustomInstantiator(f => Generator.Create(
                name, power, fuel, fuelTypeName));
    }
    
    
}