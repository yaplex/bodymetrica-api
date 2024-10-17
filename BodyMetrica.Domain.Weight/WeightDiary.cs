using BodyMetrica.Domain.Common.Models;
using BodyMetrica.Domain.Weight.Dtos;
using BodyMetrica.Domain.Weight.Persistence;

namespace BodyMetrica.Domain.Weight;

public class WeightDiary
{
    private readonly IEnumerable<WeightLogRecord> _weightLogs;

    public WeightDiary(IEnumerable<WeightLogRecord> weightLogs)
    {
        _weightLogs = weightLogs;
    }

    public IEnumerable<WeightLogDto> GetWeightLogsForUser(UserProfile user)
    {
        var result = new List<WeightLogDto>();
        foreach (var logRecord in _weightLogs)
        {
            result.Add(new WeightLogDto(){Id = logRecord.Id, RecordDate = logRecord.RecordDate, Weight = WeightInKgToWeight(logRecord.WeightInKg, user)});
        }

        return result;
    }

    private static decimal WeightInKgToWeight(decimal weightInKg, UserProfile user)
    {
        if (user.WeightUnits == WeightUnits.Kg)
        {
            return weightInKg;
        }

        return weightInKg * 2.20462m; // Convert to lbs
    }

}