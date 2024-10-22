using BodyMetrica.Contracts.Weight.Dtos;
using MediatR;

namespace BodyMetrica.Domain.Weight.Features.GetRecentWeightLogs;

public class GetWeightLogsQuery : IRequest<IEnumerable<WeightLogDto>>
{
    private const int DefaultRecordsCountToReturn = 10;
    public int RecordsCount { get; set; } = DefaultRecordsCountToReturn;
}