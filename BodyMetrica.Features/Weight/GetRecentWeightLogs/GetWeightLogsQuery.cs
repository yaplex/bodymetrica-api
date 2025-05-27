using MediatR;

namespace BodyMetrica.Features.Weight.GetRecentWeightLogs;

public class GetWeightLogsQuery : IRequest<IEnumerable<RecentWeightLogDto>>
{
    private const int DefaultRecordsCountToReturn = 10;
    public int RecordsCount { get; set; } = DefaultRecordsCountToReturn;
}