namespace BodyMetrica.Domain.Common.Models;

public class User
{
    public int Id { get; set; }
    public string ExternalId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}