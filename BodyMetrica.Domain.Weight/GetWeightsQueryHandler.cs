using AutoMapper;
using BodyMetrica.Domain.Weight.Dtos;
using BodyMetrica.Domain.Weight.Persistence;
using BodyMetrica.Domain.Weight.Repositories;
using BodyMetrica.Domain.Weight.Requests;
using BodyMetrica.Domain.Weight.Services;
using MediatR;

namespace BodyMetrica.Domain.Weight;

public class GetWeightsQueryHandler(IWeightLogRepository weightLogRepository, IUserService userService, IMapper mapper
) : IRequestHandler<GetWeightLogsQuery, IEnumerable<WeightLogDto>>
{
    public async Task<IEnumerable<WeightLogDto>> Handle(GetWeightLogsQuery request, CancellationToken cancellationToken)
    {
        var user = userService.GetCurrentUser();
        var weights = await weightLogRepository.GetWeights(user.Id);
        var weightLog = new WeightDiary(weights);
        return weightLog.GetWeightLogsForUser(user);
    }
}