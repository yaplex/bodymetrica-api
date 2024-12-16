using BodyMetrica.Features.Common;
using BodyMetrica.Features.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BodyMetrica.Features.Weight.UpdateWeightUnits;

public class UpdateWeightUnitsHandler(IUserService userService, BodyMetricaDbContext dbContext)
    : IRequestHandler<UpdateWeightUnitsCommand>
{
    public async Task Handle(UpdateWeightUnitsCommand command, CancellationToken cancellationToken)
    {
        var loggedInUser = await userService.GetLoggedInUser();

        var user = dbContext.ApplicationUsers.Single(x => x.Id == loggedInUser.Id);
        user.WeightUnits = command.WeightUnits;

        dbContext.ApplicationUsers.Update(user);
        await dbContext.SaveChangesAsync(CancellationToken.None);
    }
}