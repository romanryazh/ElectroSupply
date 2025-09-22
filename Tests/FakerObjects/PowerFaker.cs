using Bogus;
using ElectroSupply.Domain.ValueObjects;

namespace Tests.FakerObjects;

public sealed class PowerFaker : Faker<Power>
{
    public PowerFaker()
    {
        CustomInstantiator(f => new Power(f.Random.Double(min: 0)));
    }
}