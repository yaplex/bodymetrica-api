using Azure;
using BodyMetrica.Contracts.Weight.Dtos;
using BodyMetrica.Domain.Weight.Features.GetUserProfile;
using BodyMetrica.Domain.Weight.Features.UpdateWeightUnits;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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