namespace BodyMetrica.Domain.Common.Models;

public interface IAggregateRoot
{
    int Id { get; }
    int OwnerId { get; }

}