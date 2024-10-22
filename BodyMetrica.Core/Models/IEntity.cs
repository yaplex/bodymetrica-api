namespace BodyMetrica.Domain.Common.Models;

public interface IEntity
{
    int Id { get; }
    int OwnerId { get; }
    bool IsValid();
}