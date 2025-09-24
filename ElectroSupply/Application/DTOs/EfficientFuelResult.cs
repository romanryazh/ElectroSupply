using ElectroSupply.Application.Interfaces;
using ElectroSupply.Domain.Interfaces;

namespace ElectroSupply.Application.DTOs;

/// <summary>
/// Результат расчёта эффективного энергообеспечения
/// </summary>
/// <param name="Generators">Коллекция используемых генераторов</param>
/// <param name="TotalFuel">Итоговое количество требуемого топлива</param>
public record EfficientFuelResult(IReadOnlyCollection<IGenerator> Generators, double TotalFuel) : IResult;