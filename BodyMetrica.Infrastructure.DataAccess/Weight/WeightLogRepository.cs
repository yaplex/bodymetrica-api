using AutoMapper;
using BodyMetrica.Domain.Weight.Entities;
using BodyMetrica.Domain.Weight.Repositories;
using BodyMetrica.Infrastructure.DataAccess.Weight.Records;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BodyMetrica.Infrastructure.DataAccess.Weight;

public class WeightLogRepository(BodyMetricaDbContext dbContext, ILogger logger, IMapper mapper) : IWeightLogRepository
{
    public async Task<Result> AddNew(WeightLog weightLog)
    {
        try
        {
            if (!weightLog.IsValid())
                return Result.Fail("Validation failed! Unable to add new WeightLog record.");

            var weightLogRecord = mapper.Map<WeightLogRecord>(weightLog);

            await dbContext.WeightLogs.AddAsync(weightLogRecord);
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

    public async Task<IEnumerable<WeightLog>> GetWeightLogs(int ownerId, int recordsCount)
    {
        var weightLogRecords =  await dbContext.WeightLogs
            .Where(x => x.OwnerId == ownerId)
            .OrderByDescending(x => x.RecordDate)
            .Take(recordsCount)
            .ToListAsync();
        var weightLogs = mapper.Map<IEnumerable<WeightLog>>(weightLogRecords);
        return weightLogs;
    }
}