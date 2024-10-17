using AutoMapper;
using BodyMetrica.Api.Models.Weight;
using BodyMetrica.Domain.Weight;
using BodyMetrica.Domain.Weight.Dtos;
using BodyMetrica.Domain.Weight.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BodyMetrica.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeightLogController(IMediator mediator)
    : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LogWeightRequest request)
    {
        var cmd = new AddNewLogWeightRequest(request.Weight, request.RecordDate);
        var result = await mediator.Send(cmd);
        if (result.IsFailed) 
            return BadRequest(result.Errors);

        return Ok();
    }

    // [HttpGet]
    // public async Task<IEnumerable<WeightLogDto>> Get()
    // {
    //     var query = new GetWeightLogsQuery();
    //     var weightLog = await mediator.Send(query);
    //
    //     return mapper.Map<IEnumerable<WeightLogDto>>(weightLog);
    // }
}