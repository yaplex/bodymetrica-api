using BodyMetrica.Domain.Weight.Persistence;
using BodyMetrica.Domain.Weight.Repositories;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BodyMetrica.Infrastructure.DataAccess.Weight;

public class WeightLogRepository(BodyMetricaDbContext dbContext, ILogger logger) : IWeightLogRepository
{
    public async Task<Result> AddNew(WeightLogRecord weightLog)
    {
        try
        {
            await dbContext.WeightLogs.AddAsync(weightLog);
            await dbContext.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            var message = "Failed to add new WeightConverter record.";
            logger.Error(ex, message);
            return Result.Fail(message);
        }
    }

    async Task<IEnumerable<WeightLogRecord>> IWeightLogRepository.GetWeights(int userId)
    {
        return await dbContext.WeightLogs
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.RecordDate)
            .Take(10)
            .ToListAsync();
    }
}