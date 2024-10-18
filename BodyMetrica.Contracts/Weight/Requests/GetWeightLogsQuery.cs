using BodyMetrica.Contracts.Weight.Dtos;
using MediatR;

namespace BodyMetrica.Contracts.Weight.Requests;

public class GetWeightLogsQuery : IRequest<IEnumerable<WeightLogDto>>
{
    private const int DefaultRecordsCountToReturn = 10;
    public int RecordsCount { get; set; } = DefaultRecordsCountToReturn;
}