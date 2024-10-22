using BodyMetrica.Core.Repositories;
using BodyMetrica.Core.Services;
using MediatR;

namespace BodyMetrica.Domain.Weight.Features.UpdateWeightUnits;

public class UpdateWeightUnitsHandler(IUserService userService, IUserRepository userRepository)
    : IRequestHandler<UpdateWeightUnitsCommand>
{
    public async Task Handle(UpdateWeightUnitsCommand command, CancellationToken cancellationToken)
    {
        var user = userService.GetCurrentUser();
        var userFromRepo = await userRepository.GetById(user.Id);
        userFromRepo.WeightUnits = command.WeightUnits;
        await userRepository.Update(userFromRepo);
    }
}