namespace BodyMetrica.Features.Persistence;

public interface IRecord
{
    int Id { get; }
    int OwnerId { get; }
    bool IsValid();
}