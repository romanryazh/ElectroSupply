using Bogus;
using ElectroSupply.Domain.Entities;

namespace Tests.FakerObjects;

public sealed class GeneratorFaker : Faker<Generator>
{
    public GeneratorFaker()
    {
        CustomInstantiator(f => Generator.Create(
            f.Commerce.Product(),
            f.Random.Double(min: 0),
            f.Random.Double(min: 0),
            f.Commerce.Product()
            ));
    }
}