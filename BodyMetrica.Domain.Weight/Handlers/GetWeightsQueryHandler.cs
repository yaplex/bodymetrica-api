﻿using AutoMapper;
using BodyMetrica.Contracts.Weight.Dtos;
using BodyMetrica.Contracts.Weight.Requests;
using BodyMetrica.Domain.Common.Services;
using BodyMetrica.Domain.Weight.Repositories;
using MediatR;

namespace BodyMetrica.Domain.Weight.Handlers;

public class GetWeightsQueryHandler(
    IWeightLogRepository weightLogRepository,
    IUserService userService,
    IMapper mapper
) : IRequestHandler<GetWeightLogsQuery, IEnumerable<WeightLogDto>>
{
    public async Task<IEnumerable<WeightLogDto>> Handle(GetWeightLogsQuery request,
        CancellationToken cancellationToken)
    {
        var user = userService.GetCurrentUser();
        var weightLogs = await weightLogRepository.GetWeightLogs(user.Id, request.RecordsCount);

        return mapper.Map<IEnumerable<WeightLogDto>>(weightLogs);
    }
}