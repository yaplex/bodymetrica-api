using BodyMetrica.Domain.Weight;

namespace BodyMetrica.Api.Models.Weight;

public class AddWeightRequest
{
    public decimal Weight { get; set; }
    public WeightUnits Units { get; set; }
    public DateTimeOffset Date { get; set; }
}