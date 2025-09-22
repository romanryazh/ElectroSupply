using Bogus;
using ElectroSupply.Domain.ValueObjects;

namespace Tests.FakerObjects;

public sealed class PeriodFaker : Faker<Period>
{
    public PeriodFaker()
    {
        CustomInstantiator(f => new Period(f.Random.Int(min: 1)));
    }
}