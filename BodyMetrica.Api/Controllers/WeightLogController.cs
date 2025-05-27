using BodyMetrica.Features.Weight.AddNewWeightLog;
using BodyMetrica.Features.Weight.GetRecentWeightLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BodyMetrica.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeightLogController(IMediator mediator)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddNewWeightLogRequest request)
    {
        var result = await mediator.Send(request);
        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok();
    }

    [HttpGet]
    public async Task<IEnumerable<RecentWeightLogDto>> Get()
    {
        var query = new GetWeightLogsQuery { RecordsCount = 7 };
        var weightLogs = await mediator.Send(query);
        return weightLogs;
    }
}