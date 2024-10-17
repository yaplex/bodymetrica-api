using BodyMetrica.Domain.Common.Models;

namespace BodyMetrica.Domain.Weight;

public class WeightConverter
{
    public static decimal WeightInKgToWeight(decimal weightInKg, WeightUnits toUnits)
    {
        if (toUnits == WeightUnits.Kg)
            return weightInKg;

        return weightInKg * 2.20462m; // Convert to lbs
    }
}