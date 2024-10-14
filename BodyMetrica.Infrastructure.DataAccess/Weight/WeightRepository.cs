using BodyMetrica.Domain.Weight.Repositories;
using FluentResults;

namespace BodyMetrica.Infrastructure.DataAccess.Weight;

public class WeightRepository(BodyMetricaDbContext dbContext): IWeightRepository
{
    public async Task<Result> AddNew(Domain.Weight.Weight weight, DateTimeOffset dateTime)
    {
        var weightRecord = new WeightRecord() { WeightInKg = weight.WeightInKg, RecordDate = dateTime };
        var result = await dbContext.Weights.AddAsync(weightRecord);

        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }
}