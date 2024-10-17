namespace BodyMetrica.Domain.Weight.Dtos;

public class WeightLogDto
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public decimal Weight { get; set; }
    public DateTimeOffset RecordDate { get; set; }
}