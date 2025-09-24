using ElectroSupply.Application.Enums;
using ElectroSupply.Application.Strategies;
using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;
using ElectroSupply.UI;

namespace ElectroSupply.Application;

/// <summary>
/// Презентационный сервис программы с полным циклом работы приложения.
/// Использует сервисы <see cref="IUserInterface"/> (для взаимодействия), <see cref="ICalculator{TResult}"/> (для расчётов)
/// и агрегат <see cref="IPowerStation"/> в качестве зависимостей через DI
/// </summary>
public class PowerStationApp
{
    private readonly IUserInterface _ui;
    private readonly ICalculator _calculator;
    private readonly IPowerStation _station;
    private readonly IFuelTypeRepository _fuelTypeRepository;
    private bool _isRunning;

    /// <summary>
    /// Создаёт экземпляр <see cref="PowerStationApp"/>
    /// </summary>
    /// <param name="ui">Пользовательский интерфейс для взаимодействия с приложением</param>
    /// <param name="calculator">Сервис для расчётов</param>
    /// <param name="station">Электростанция</param>
    public PowerStationApp(IUserInterface ui, ICalculator calculator, IPowerStation station,
        IFuelTypeRepository fuelTypeRepository)
    {
        ArgumentNullException.ThrowIfNull(ui);
        ArgumentNullException.ThrowIfNull(calculator);
        ArgumentNullException.ThrowIfNull(station);

        _ui = ui;
        _calculator = calculator;
        _station = station;
        _fuelTypeRepository = fuelTypeRepository;
        _isRunning = true;
    }

    /// <summary>
    /// Запускает основной поток выполнения программы и все её модули
    /// </summary>
    public void Run()
    {
        Console.WriteLine("Обеспечиваем город Череповец электричеством!\n");

        while (_isRunning)
        {
            var operation = _ui.ReadOperation();

            switch (operation)
            {
                case (int)OperationType.AddFuelType:
                    try
                    {
                        var fuelType = _ui.ReadFuelType();
                        if (_fuelTypeRepository.FuelTypeExists(fuelType.Name))
                        {
                            _ui.DisplayError("Такой тип топлива уже существует");
                        }

                        _fuelTypeRepository.AddFuelType(fuelType);
                    }
                    catch (Exception ex)
                    {
                        _ui.DisplayError(ex.Message);
                    }

                    break;
                case (int)OperationType.Efficiency:
                    try
                    {
                        var queryData = GetQueryData();
                        _station.AddRangeGenerators(queryData.Item3);
                        _calculator.SetStrategy(new GreaterEfficiencyFuelCalculationStrategy());
                        var result = _calculator.Calculate(queryData.power, queryData.period, queryData.Item3);
                        _ui.DisplayResult(result);
                    }
                    catch (Exception ex)
                    {
                        _ui.DisplayError(ex.Message);
                    }

                    break;
                case (int)OperationType.Cheaper:
                    try
                    {
                        var queryData = GetQueryData();
                        _station.AddRangeGenerators(queryData.Item3);
                        _calculator.SetStrategy(
                            new CheaperPriceCalculationStrategy(_fuelTypeRepository.GetFuelTypes()));
                        var result = _calculator.Calculate(queryData.power, queryData.period, queryData.Item3);
                        _ui.DisplayResult(result);
                    }
                    catch (Exception ex)
                    {
                        _ui.DisplayError(ex.Message);
                    }

                    break;
                case (int)OperationType.Exit:
                    _isRunning = false;
                    break;
            }
        }
    }

    /// <summary>
    /// Получает данные запроса
    /// </summary>
    /// <returns>Возвращает данные запроса - мощность, период (дни), коллекцию генераторов</returns>
    private (Power power, Period period, IReadOnlyCollection<IGenerator>) GetQueryData()
    {
        var power = _ui.ReadRequiredPower();
        var days = _ui.ReadRequiredDays();
        var generators = _ui.ReadGenerators();
        return (power, days, generators);
    }
}