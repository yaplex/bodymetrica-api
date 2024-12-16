namespace BodyMetrica.Features.Persistence;

public class WeightLogRecord: BodyMetricaRecord
{
    public decimal Weight { get; set; }
    public DateTimeOffset RecordDate { get; set; }
}