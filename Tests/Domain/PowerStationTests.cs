using Bogus;
using ElectroSupply.Domain.Entities;
using FluentAssertions;

namespace Tests.Domain;

public class PowerStationTests
{
    private readonly Faker _faker = new();

    public PowerStationTests()
    {
        
    }

    [Fact]
    public void Create_WithValidData_ShouldCreatesValidEntity()
    {
        var name = _faker.Commerce.ProductName();

        var powerStation = new Faker<PowerStation>()
            .CustomInstantiator(f => PowerStation.Create(name)).Generate();
        
        powerStation.Should().NotBeNull();
        powerStation.Name.Should().Be(name);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_WithInvalidName_ShouldThrowException(string invalidName)
    {
        var powerStationFaker = new Faker<PowerStation>()
            .CustomInstantiator(f => PowerStation.Create(invalidName));
        
        var action = () => powerStationFaker.Generate();

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void AddGenerator_WithValidData_ShouldAddGenerator()
    {
        var name = _faker.Commerce.ProductName();
        var powerStation = new Faker<PowerStation>()
            .CustomInstantiator(f => PowerStation.Create(name)).Generate();
        var generator = GetCustomGeneratorFaker().Generate();
        
        powerStation.AddGenerator(generator);

        powerStation.Generators.Should().HaveCount(1).And.Contain(generator);
    }

    [Fact]
    public void AddGenerator_Duplicates_ShouldThrowException()
    {
        var powerStationName = _faker.Commerce.ProductName();
        var powerStation = new Faker<PowerStation>()
            .CustomInstantiator(f => PowerStation.Create(powerStationName)).Generate();
        var generatorName = _faker.Commerce.ProductName();
        var generator = new Faker<Generator>()
            .CustomInstantiator(f => Generator.Create(
                generatorName,
                _faker.Random.Double(),
                _faker.Random.Double(),
                _faker.Commerce.ProductName())).Generate();     
        powerStation.AddGenerator(generator);
        
        var action = () => powerStation.AddGenerator(generator);
        
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void AddGenerator_WithNull_ShouldThrowException()
    {
        var powerStationName = _faker.Commerce.ProductName();
        var powerStation = new Faker<PowerStation>()
            .CustomInstantiator(f => PowerStation.Create(powerStationName)).Generate();    
        
        var action = () => powerStation.AddGenerator(null);
        
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void AddGeneratorRange_WithValidData_ShouldAddGeneratorRange()
    {
        var powerStationName = _faker.Commerce.ProductName();
        var powerStation = new Faker<PowerStation>()
            .CustomInstantiator(f => PowerStation.Create(powerStationName)).Generate();
        var generator1 = GetCustomGeneratorFaker().Generate();
        var generator2 = GetCustomGeneratorFaker().Generate();
        
        powerStation.AddRangeGenerators([generator1, generator2]);
        
        powerStation.Generators.Should().HaveCount(2);
        powerStation.Generators.Should().Contain(generator1);
        powerStation.Generators.Should().Contain(generator2);
    }
    
    private Faker<Generator> GetCustomGeneratorFaker(Func<string>? nameGen = null, 
        Func<double>? powerGen = null, Func<double>? fuelGen = null, Func<string>? fuelTypeNameGen = null)
    {
        var name = nameGen != null ? nameGen() : _faker.Commerce.Product();
        var power = powerGen != null ? powerGen() : _faker.Random.Double();
        var fuel = fuelGen != null ? fuelGen() : _faker.Random.Double();
        var fuelTypeName = fuelTypeNameGen != null ? fuelTypeNameGen() : _faker.Commerce.Product();

        return new Faker<Generator>()
            .CustomInstantiator(f => Generator.Create(
                name, power, fuel, fuelTypeName));
    }
    
}