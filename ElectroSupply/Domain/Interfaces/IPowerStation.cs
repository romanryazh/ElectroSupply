namespace ElectroSupply.Domain.Interfaces;

public interface IPowerStation
{
    public string Name { get; }
    
    public IReadOnlyCollection<IGenerator> Generators { get; }
    
    public void AddGenerator(IGenerator generator);
    
    public void AddRangeGenerators(IEnumerable<IGenerator> generators);
}