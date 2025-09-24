namespace ElectroSupply.Application.Enums;

/// <summary>
/// Тип операции выбранной пользователем
/// </summary>
public enum OperationType
{
    /// <summary>
    /// Добавить тип топлива
    /// </summary>
    AddFuelType = 1,
    /// <summary>
    /// Расчёт энергообеспечения по топливу
    /// </summary>
    Efficiency = 2,
    /// <summary>
    /// Расчёт дешёвого энергообеспечения по цене
    /// </summary>
    Cheaper = 3,
    /// <summary>
    /// Выход из программы
    /// </summary>
    Exit = 4,
}