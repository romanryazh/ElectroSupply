using Bogus;
using ElectroSupply.Application.DTOs;
using ElectroSupply.Application.Interfaces;
using ElectroSupply.Application.Services;
using ElectroSupply.Domain.Entities;
using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Tests.FakerObjects;

namespace Tests.Application;

public class CalculatorTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void Calculate_WithValidData_ShouldReturnCalculator()
    {
        var power = new PowerFaker().Generate();
        var period = new PeriodFaker().Generate();
        var generator = new GeneratorFaker().Generate();
        List<IGenerator> generators = [generator];
        var strategyMoq = new Mock<ICalculationStrategy>();
        strategyMoq.Setup(strategy => strategy.Calculate(power, period, generators)).Returns(new CheaperPriceResult(generators, _faker.Random.Decimal()));
        var calculator = new Calculator(strategyMoq.Object);

        var result = calculator.Calculate(power, period, [generator]);
        
        result.Should().NotBeNull();
        result.Should().BeOfType<CheaperPriceResult>();
        result.Generators.Should().BeEquivalentTo(generators);
    }
}