namespace BodyMetrica.Features.Persistence;

public abstract class BodyMetricaRecord : IRecord
{
    public int Id { get; protected set; }
    public int OwnerId { get; set; }

    public bool IsValid()
    {
        return OwnerId > 0;
    }
}