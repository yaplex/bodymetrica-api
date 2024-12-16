using BodyMetrica.Features.Common;
using BodyMetrica.Features.Persistence;
using FluentResults;
using MediatR;
using Serilog;

namespace BodyMetrica.Features.BloodPressure.AddNewBloodPressureLog;

public class LogNewBloodPressureCommandHandler(BodyMetricaDbContext db, IUserService user, ILogger log)
    : IRequestHandler<LogNewBloodPressureCommand, Result>
{
    public async Task<Result> Handle(LogNewBloodPressureCommand request, CancellationToken cancellationToken)
    {
        var loggedInUser = await user.GetLoggedInUser();
        try
        {
            var record = new BloodPressureLogRecord
            {
                OwnerId = loggedInUser.Id,
                Systolic = request.Systolic,
                Diastolic = request.Diastolic,
                Pulse = request.Pulse,
                Note = request.Note,
                RecordDate = request.RecordDate
            };
            if (record.IsValid())
            {
                await db.BloodPressureLogs.AddAsync(record, CancellationToken.None);
                await db.SaveChangesAsync(CancellationToken.None);
            }
            else
            {
                return Result.Fail("Invalid record.");
            }

            return Result.Ok();
        }
        catch (Exception e)
        {
            var error = "Failed to add blood pressure log";
            log.Error(e, error);
            return Result.Fail(error);
        }
    }
}