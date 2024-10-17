using AutoMapper;
using BodyMetrica.Domain.Weight.Persistence;
using BodyMetrica.Domain.Weight.Repositories;
using BodyMetrica.Domain.Weight.Services;
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
            var message = "Failed to add new WeightLog record.";
            logger.Error(ex, message);
            return Result.Fail(message);
        }
    }

    Task<IEnumerable<WeightLogRecord>> IWeightLogRepository.GetWeights()
    {
        // var logRecords = await dbContext.WeightLogs
        //     .Where(x => x.UserId == user.UserId)
        //     .OrderBy(x => x.RecordDate)
        //     .Take(10)
        //     .ToListAsync();
        // return mapper.Map<IEnumerable<WeightLog>>(logRecords);
        throw new NotImplementedException();
    }
}