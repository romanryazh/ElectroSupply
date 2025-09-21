using ElectroSupply.Domain.Interfaces;

namespace ElectroSupply.Application.Interfaces;

public interface IResult
{
    IReadOnlyCollection<IGenerator> Generators { get; }
}