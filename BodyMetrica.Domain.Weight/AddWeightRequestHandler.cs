using BodyMetrica.Domain.Weight.Persistence;
using BodyMetrica.Domain.Weight.Repositories;
using BodyMetrica.Domain.Weight.Requests;
using BodyMetrica.Domain.Weight.Services;
using FluentResults;
using MediatR;

namespace BodyMetrica.Domain.Weight;

public class AddWeightRequestHandler(
    IWeightLogRepository weightLogRepository,
    IUserService user) : IRequestHandler<AddNewLogWeightRequest, Result>
{
    public async Task<Result> Handle(AddNewLogWeightRequest request, CancellationToken cancellationToken)
    {
        request.SetUserProfile(user.GetCurrentUser());
        
        var result = await weightLogRepository.AddNew(new WeightLogRecord
            { UserId = request.UserId, RecordDate = request.RecordDate, WeightInKg = request.WeightInKg });
        return result;
    }
}