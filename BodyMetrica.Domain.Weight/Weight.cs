using BodyMetrica.Domain.Common;

namespace BodyMetrica.Domain.Weight;

public class Weight(decimal weightInKg) : ValueObject
{
    public decimal WeightInKg { get; } = weightInKg;

    public static Weight Create(decimal weight, WeightUnits units)
    {
        if (units == WeightUnits.Kg)
        {
            return new Weight(weight);
        }

        throw new NotImplementedException();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return WeightInKg;
    }
}
