using BodyMetrica.Domain.Weight.Persistence;
using BodyMetrica.Domain.Weight.Repositories;
using BodyMetrica.Domain.Weight.Requests;
using MediatR;

namespace BodyMetrica.Domain.Weight;

public class GetWeightsQueryHandler(
    IWeightLogRepository weightLogRepository
) : IRequestHandler<GetWeightLogsQuery, IEnumerable<WeightLogRecord>>
{
    public async Task<IEnumerable<WeightLogRecord>> Handle(GetWeightLogsQuery request,
        CancellationToken cancellationToken)
    {
        return await weightLogRepository.GetWeights(request.UserId);
    }
}