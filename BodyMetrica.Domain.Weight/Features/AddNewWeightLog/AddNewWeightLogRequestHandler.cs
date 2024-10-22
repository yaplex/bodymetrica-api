using BodyMetrica.Contracts.Weight.Requests;
using BodyMetrica.Domain.Common.Services;
using BodyMetrica.Domain.Weight.Entities;
using BodyMetrica.Domain.Weight.Repositories;
using FluentResults;
using MediatR;

namespace BodyMetrica.Domain.Weight.Features.AddNewWeightLog;

public class AddNewWeightLogRequestHandler(
    IWeightLogRepository weightLogRepository,
    IUserService userService) : IRequestHandler<AddNewWeightLogRequest, Result>
{
    public async Task<Result> Handle(AddNewWeightLogRequest request, CancellationToken cancellationToken)
    {
        var user = userService.GetCurrentUser();
        var weight = new WeightLog(request.Weight, request.RecordDate);
        weight.SetOwner(user.Id);

        var result = await weightLogRepository.AddNew(weight);
        return result;
    }
}