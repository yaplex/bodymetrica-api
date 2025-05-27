namespace BodyMetrica.Features.Weight.GetRecentWeightLogs;

public class RecentWeightLogDto
{
    public int Id { get; set; }
    public decimal Weight { get; set; }
    public DateTimeOffset RecordDate { get; set; }

}