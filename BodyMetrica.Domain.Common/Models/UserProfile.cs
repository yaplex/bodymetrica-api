namespace BodyMetrica.Domain.Common.Models;

public class UserProfile
{
    public int Id { get; set; }
    public string ExternalId { get; set; }
    public WeightUnits WeightUnits { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}