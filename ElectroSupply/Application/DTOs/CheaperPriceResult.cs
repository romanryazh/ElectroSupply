using ElectroSupply.Application.Interfaces;
using ElectroSupply.Domain.Interfaces;

namespace ElectroSupply.Application.DTOs;

public record CheaperPriceResult(IReadOnlyCollection<IGenerator> Generators, decimal TotalPrice) : IResult;