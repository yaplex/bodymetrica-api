namespace BodyMetrica.Features.Persistence;

public class BloodPressureLogRecord : BodyMetricaRecord
{
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public int Pulse { get; set; }
    public string? Note { get; set; }
    public DateTimeOffset RecordDate { get; set; }
}