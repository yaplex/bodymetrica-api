namespace BodyMetrica.Api.Models.Weight;

public class WeightLogDto
{
    public long Id { get; set; }
    public decimal Weight { get; set; }
    public DateTimeOffset RecordDate { get; set; }
}