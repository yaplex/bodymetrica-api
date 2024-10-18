namespace BodyMetrica.Infrastructure.DataAccess.Weight.Records;

public class WeightLogRecord
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public decimal Weight { get; set; }
    public DateTimeOffset RecordDate { get; set; }
}