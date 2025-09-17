using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.UI;

/// <summary>
/// Пользовательский интерфейс для взаимодействия с приложением
/// </summary>
public interface IUserInterface
{
    /// <summary>
    /// Считывает генераторы от пользователя
    /// </summary>
    /// <returns>Предоставленные генераторы</returns>
    public IReadOnlyCollection<IGenerator> ReadGenerators();

    /// <summary>
    /// Считывает требуемую мощность от пользователя
    /// </summary>
    /// <returns>Требуемая мощность</returns>
    public Power ReadRequiredPower();

    /// <summary>
    /// Считывает требуемое количество дней от пользователя
    /// </summary>
    /// <returns>Количество дней</returns>
    public Days ReadRequiredDays();
    
    /// <summary>
    /// Выводит результат
    /// </summary>
    /// <param name="generators">Выбранные генераторы</param>
    /// <param name="totalFuel">Требуемое общее топливо</param>
    public void DisplayResult(IReadOnlyCollection<IGenerator> generators, Fuel totalFuel);
    
    /// <summary>
    /// Выводит сообщение об ошибке
    /// </summary>
    /// <param name="message">Текст сообщения</param>
    public void DisplayError(string message);
}