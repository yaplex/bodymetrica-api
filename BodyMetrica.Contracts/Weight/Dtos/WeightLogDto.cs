namespace BodyMetrica.Contracts.Weight.Dtos;

public class WeightLogDto
{
    public int Id { get; set; }
    public decimal Weight { get; set; }
    public DateTimeOffset RecordDate { get; set; }

}