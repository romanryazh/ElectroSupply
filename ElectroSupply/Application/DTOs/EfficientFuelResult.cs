using ElectroSupply.Application.Interfaces;
using ElectroSupply.Domain.Interfaces;

namespace ElectroSupply.Application.DTOs;

public record EfficientFuelResult(IReadOnlyCollection<IGenerator> Generators, double TotalFuel) : IResult;