using BodyMetrica.Features.Common;
using BodyMetrica.Features.Persistence;
using FluentResults;
using MediatR;
using Serilog;

namespace BodyMetrica.Features.Weight.AddNewWeightLog;

public class AddNewWeightLogRequestHandler(
    IUserService userService,
    BodyMetricaDbContext dbContext,
    ILogger logger) : IRequestHandler<AddNewWeightLogRequest, Result>
{
    public async Task<Result> Handle(AddNewWeightLogRequest request, CancellationToken cancellationToken)
    {
        var user = await userService.GetLoggedInUser();

        try
        {
            var weightLogRecord = new WeightLogRecord
            {
                OwnerId = user.Id,
                Weight = request.Weight,
                RecordDate = request.RecordDate
            };

            if (weightLogRecord.IsValid())
            {
                await dbContext.WeightLogs.AddAsync(weightLogRecord, CancellationToken.None);
                await dbContext.SaveChangesAsync(CancellationToken.None);
            }
            else
            {
                return Result.Fail("Invalid record.");
            }

            return Result.Ok();
        }
        catch (Exception ex)
        {
            var message = "Failed to add new WeightLog record.";
            logger.Error(ex, message);
            return Result.Fail(message);
        }
    }
}