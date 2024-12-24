using MediatR;

namespace BodyMetrica.Features.BloodPressure.GetRecentBloodPressureLogs;

public class GetRecentBloodPressureLogsQuery: IRequest<IEnumerable<RecentBloodPressureLogDto>>
{
    public int RecordsCount { get; set; }
}