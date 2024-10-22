namespace BodyMetrica.Domain.Common.Models;

public abstract class BodyMetricaEntity : IEntity
{
    public abstract int Id { get; protected set; }
    public abstract int OwnerId { get; protected set; }

    public bool IsValid()
    {
        return OwnerId > 0;
    }
}