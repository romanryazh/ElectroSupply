using ElectroSupply.Application.Services;
using ElectroSupply.Application.Strategies;
using ElectroSupply.Domain.Entities;

var station = PowerStation.Create("Подольская ТЭС");

station.AddRangeGenerators(
[
    Generator.Create("ЗАЕБАТОМ 1/3-123Г", 10.5, 5),
    Generator.Create("ЗАЕБАТОМ ЭС", 30, 6),
    Generator.Create("ЗАЕБАТОМ ОУ", 19, 3)
]);

var strategy = new GreaterEfficiencyFuelCalculationStrategy();
var calculator = new FuelCalculator(strategy);

var result = calculator.CalculateRequiredFuel(45, 5, station.Generators);

Console.WriteLine($"Выбраны генераторы: {string.Join(", ", result.Item1.Select(x => x.Name.ToString()))}");
Console.WriteLine($"Нужно топлива: {result.totalFuel.Value} л");

Console.ReadKey(); 