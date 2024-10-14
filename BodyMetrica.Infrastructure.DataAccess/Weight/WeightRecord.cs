namespace BodyMetrica.Infrastructure.DataAccess.Weight;

public class WeightRecord
{
    public int Id { get; set; }
    public decimal WeightInKg { get; set; }
    public DateTimeOffset RecordDate { get; set; }
}