namespace BodyMetrica.Features.BloodPressure.GetRecentBloodPressureLogs;

public class RecentBloodPressureLogDto
{
    public int Id { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public int Pulse { get; set; }
    public string? Note { get; set; }
    public DateTimeOffset RecordDate { get; set; }

}