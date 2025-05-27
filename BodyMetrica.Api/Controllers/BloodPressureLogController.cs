using BodyMetrica.Features.BloodPressure.AddNewBloodPressureLog;
using BodyMetrica.Features.BloodPressure.GetRecentBloodPressureLogs;
using BodyMetrica.Features.Weight.GetRecentWeightLogs;
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

    [HttpGet]
    public async Task<IEnumerable<RecentBloodPressureLogDto>> Get()
    {
        var query = new GetRecentBloodPressureLogsQuery() { RecordsCount = 7 };
        return await mediator.Send(query);
    }
}