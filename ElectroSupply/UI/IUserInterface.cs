using ElectroSupply.Domain.Interfaces;
using ElectroSupply.Domain.ValueObjects;

namespace ElectroSupply.UI;

public interface IUserInterface
{
    /// <summary>
    /// Считать генераторы от пользователя
    /// </summary>
    /// <returns>Предоставленные генераторы</returns>
    public IReadOnlyCollection<IGenerator> ReadGenerators();

    /// <summary>
    /// Считать требуемую мощность от пользователя
    /// </summary>
    /// <returns>Требуемая мощность</returns>
    public Power ReadRequiredPower();

    /// <summary>
    /// Считать требуемое количество дней от пользователя
    /// </summary>
    /// <returns>Количество дней</returns>
    public Days ReadRequiredDays();
    
    /// <summary>
    /// Вывести результат
    /// </summary>
    /// <param name="generators">Выбранные генераторы</param>
    /// <param name="totalFuel">Требуемое общее топливо</param>
    public void DisplayResult(IReadOnlyCollection<IGenerator> generators, Fuel totalFuel);
}