using BodyMetrica.Contracts.Weight.Dtos;
using BodyMetrica.Contracts.Weight.Requests;
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
    public async Task<IEnumerable<WeightLogDto>> Get()
    {
        var query = new GetWeightLogsQuery();
        var weightLogs = await mediator.Send(query);
        return weightLogs;
    }
}