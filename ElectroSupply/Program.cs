using ElectroSupply.Application;
using ElectroSupply.Application.Services;
using ElectroSupply.Application.Strategies;
using ElectroSupply.Domain.Entities;
using ElectroSupply.UI.ConsoleUI;

// Программа вычисляет литраж топлива, необходимого для работы генераторов в указанный
// промежуток времени.
//
// -> На вход подаётся требуемая мощность, количество дней и список генераторов
// <- На выходе программа выводит список работающих генераторов и суммарное топливо для них
//
// Приложение поделено на несколько слоёв:
// - Предметный (Domain)
// - Слой приложения (Application)
// - Пользовательский интерфейс (UI)
//
// Каждый класс имеет одну ответственность, зависимости реализованы через интерфейсы.
// В качестве главной сущности, агрегата, использован класс PowerStation.
// Основные мерные значения вынесены в Value Object'ы.
// Выборка используемых генераторов реализована через паттерн Стратегия для удобного расширения.
// Реализованная стратегия использует выборку самых эффективных генераторов по соотношению мощность/топливо.
// Интерфейс пользователя представлен в абстрактном виде, реализован в виде консольного ввода-вывода.
// Главный сервис приложения (PowerStationApp) использует прочие зависимости и запускает весь конвейер программы.

var station = PowerStation.Create("Череповецкая АЭС");
var strategy = new GreaterEfficiencyFuelCalculationStrategy();
var calculator = new FuelCalculator(strategy);
var ui = new ConsoleUserInterface();
var app = new PowerStationApp(ui, calculator, station);

app.Run();
