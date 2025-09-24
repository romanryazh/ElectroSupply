using ElectroSupply.Application.Interfaces;
using ElectroSupply.Domain.Interfaces;

namespace ElectroSupply.Application.DTOs;

/// <summary>
/// Результат расчёта наиболее дешёвого энергообеспечения
/// </summary>
/// <param name="Generators">Коллекция используемых генераторов</param>
/// <param name="TotalPrice">Итоговая стоимость</param>
public record CheaperPriceResult(IReadOnlyCollection<IGenerator> Generators, decimal TotalPrice) : IResult;