using FluentResults;

namespace BodyMetrica.Domain.Weight.Repositories;

public interface IWeightRepository
{
    Task<Result> AddNew(Weight weight, DateTimeOffset dateTime);
    Task<IEnumerable<Weight>> GetWeights();
}