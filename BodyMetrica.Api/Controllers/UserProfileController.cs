using BodyMetrica.Features.User.GetUserProfile;
using BodyMetrica.Features.Weight.UpdateWeightUnits;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BodyMetrica.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserProfileController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<UserProfileResponse> Get()
    {
        return await mediator.Send(new GetUserProfileRequest());
    }

    [HttpPost("UpdateWeightUnits")]
    public async Task<IActionResult> UpdateWeightUnits([FromBody] UpdateWeightUnitsCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}