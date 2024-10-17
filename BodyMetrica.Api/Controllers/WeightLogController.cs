using AutoMapper;
using BodyMetrica.Api.Models.Weight;
using BodyMetrica.Domain.Common.Models;
using BodyMetrica.Domain.Weight;
using BodyMetrica.Domain.Weight.Persistence;
using BodyMetrica.Domain.Weight.Requests;
using BodyMetrica.Domain.Weight.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BodyMetrica.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeightLogController(IMediator mediator, IUserService userService)
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

    [HttpGet]
    public async Task<IEnumerable<WeightLogDto>> Get()
    {
        var user = userService.GetCurrentUser();
        var query = new GetWeightLogsQuery(user.Id);
        var weightLogs = await mediator.Send(query);
        var result = new List<WeightLogDto>();
        foreach (var logRecord in weightLogs)
        {
            result.Add(new WeightLogDto()
            {
                Id = logRecord.Id, 
                RecordDate = logRecord.RecordDate, 
                Weight = WeightConverter.WeightInKgToWeight(logRecord.WeightInKg, user.WeightUnits)
            });
        }

        return result;
    }
}