using BodyMetrica.Features.BloodPressure.AddNewBloodPressureLog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BodyMetrica.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BloodPressureLogController(IMediator mediator)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LogNewBloodPressureCommand cmd)
    {
        var result = await mediator.Send(cmd);
        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok();
    }
}