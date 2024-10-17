using BodyMetrica.Domain.Weight.Persistence;
using FluentResults;

namespace BodyMetrica.Domain.Weight.Repositories;

public interface IWeightLogRepository
{
    Task<Result> AddNew(WeightLogRecord weightLog);
    Task<IEnumerable<WeightLogRecord>> GetWeights();
}