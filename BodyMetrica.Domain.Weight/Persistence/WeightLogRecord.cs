namespace BodyMetrica.Domain.Weight.Persistence;

public class WeightLogRecord
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public decimal WeightInKg { get; set; }
    public DateTimeOffset RecordDate { get; set; }
}