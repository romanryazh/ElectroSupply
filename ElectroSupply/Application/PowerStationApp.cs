using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;
using ElectroSupply.UI;

namespace ElectroSupply.Application;

public class PowerStationApp
{
    private readonly IUserInterface _ui;
    private readonly IFuelCalculator _calculator;
    private readonly IPowerStation _station;
    
    public PowerStationApp(IUserInterface ui, IFuelCalculator calculator, IPowerStation station)
    {
        ArgumentNullException.ThrowIfNull(ui);
        ArgumentNullException.ThrowIfNull(calculator);
        ArgumentNullException.ThrowIfNull(station);
        
        _ui = ui;
        _calculator = calculator;
        _station = station;
    }

    public void Run()
    {
        Console.WriteLine("Обеспечиваем город Череповец электричеством!\n");

        var power = _ui.ReadRequiredPower();
        var days = _ui.ReadRequiredDays();
        var generators = _ui.ReadGenerators();
        
        _station.AddRangeGenerators(generators);

        try
        {
            var result = _calculator.CalculateRequiredFuel(power, days, generators);
            _ui.DisplayResult(result.Item1, result.totalFuel);
        }
        catch (Exception ex)
        {
            _ui.DisplayError(ex.Message);
        }
    }
}