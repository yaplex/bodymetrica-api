using BodyMetrica.Domain.Common.Models;

namespace BodyMetrica.Domain.Weight.Entities;

// todo: this is not an AggregateRoot, It belongs to a collection, such as "Diary". Change later.
public class WeightLog(decimal weight, DateTimeOffset recordDate) : BodyMetricaEntity, IAggregateRoot
{
    public override int Id { get; protected set; }
    public override int OwnerId { get; protected set; }

    public decimal Weight { get; set; } = weight;
    public DateTimeOffset RecordDate { get; set; } = recordDate;

    public void SetOwner(int userId)
    {
        OwnerId = userId;
    }
}