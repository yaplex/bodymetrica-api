using BodyMetrica.Domain.Weight.Repositories;
using FluentResults;
using MediatR;

namespace BodyMetrica.Domain.Weight;

public class AddWeightCommand(decimal weight, WeightUnits units, DateTimeOffset dateTime): IRequest<Result>
{
    public decimal Weight { get; } = weight;
    public WeightUnits Units { get; } = units;
    public DateTimeOffset DateTime { get; } = dateTime;
}

public class AddWeightCommandHandler(IWeightRepository weightRepository): IRequestHandler<AddWeightCommand, Result>
{
    public async Task<Result> Handle(AddWeightCommand request, CancellationToken cancellationToken)
    {
        var weight = Weight.Create(request.Weight, request.Units);
        var result = await weightRepository.AddNew(weight, request.DateTime);
        return result;
    }
}