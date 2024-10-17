using BodyMetrica.Domain.Weight.Persistence;
using MediatR;

namespace BodyMetrica.Domain.Weight.Requests;

public class GetWeightLogsQuery(int userId) : IRequest<IEnumerable<WeightLogRecord>>
{
    public int UserId { get; set; } = userId;
}