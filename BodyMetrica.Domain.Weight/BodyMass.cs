namespace BodyMetrica.Domain.Weight;

public class BodyMass
{
    public int Id { get; set; }
    public decimal WeightInKg { get; set; }
    public DateTimeOffset RecordDate { get; set; }
}