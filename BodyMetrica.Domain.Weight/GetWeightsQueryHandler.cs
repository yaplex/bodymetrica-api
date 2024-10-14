using BodyMetrica.Domain.Weight.Repositories;
using MediatR;

namespace BodyMetrica.Domain.Weight;

public class GetWeightsQuery : IRequest<IEnumerable<Weight>>
{

}

public class GetWeightsQueryHandler(IWeightRepository weightRepository ): IRequestHandler<GetWeightsQuery, IEnumerable<Weight>>
{
    public Task<IEnumerable<Weight>> Handle(GetWeightsQuery request, CancellationToken cancellationToken)
    {
        var weights = weightRepository.GetWeights();
        return weights;
    }
}