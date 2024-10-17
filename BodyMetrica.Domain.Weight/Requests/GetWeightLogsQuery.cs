using BodyMetrica.Domain.Weight.Dtos;
using MediatR;

namespace BodyMetrica.Domain.Weight.Requests;

public class GetWeightLogsQuery : IRequest<IEnumerable<WeightLogDto>>
{

}
