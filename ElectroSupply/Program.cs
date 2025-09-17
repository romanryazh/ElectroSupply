using ElectroSupply.Application;
using ElectroSupply.Application.Services;
using ElectroSupply.Application.Strategies;
using ElectroSupply.Domain.Entities;
using ElectroSupply.UI.ConsoleUI;

var station = PowerStation.Create("Череповецкая АЭС");
var strategy = new GreaterEfficiencyFuelCalculationStrategy();
var calculator = new FuelCalculator(strategy);
var ui = new ConsoleUserInterface();
var app = new PowerStationApp(ui, calculator, station);

app.Run();
