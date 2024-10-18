using BodyMetrica.Domain.Common.Repositories;
using BodyMetrica.Domain.Weight.Entities;
using FluentResults;

namespace BodyMetrica.Domain.Weight.Repositories;

public interface IWeightLogRepository: IRepository<WeightLog>
{
    Task<Result> AddNew(WeightLog weightLog);
    Task<IEnumerable<WeightLog>> GetWeightLogs(int ownerId, int recordsCount);
}