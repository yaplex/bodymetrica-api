namespace BodyMetrica.Features.Persistence;

public class BodyMetricaUserRecord
{
    public int Id { get; set; }
    public string ExternalId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? WeightUnits { get; set; }
    public string? Name { get; set; }
    public string? Picture { get; set; }
    public string? Email { get; set; }
    public bool? EmailVerified { get; set; }
}