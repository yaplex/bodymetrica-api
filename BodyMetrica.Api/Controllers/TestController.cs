using BodyMetrica.Contracts.Weight.Dtos;
using BodyMetrica.Domain.Weight.Features.AddNewWeightLog;
using BodyMetrica.Domain.Weight.Features.GetRecentWeightLogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BodyMetrica.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController(IConfiguration config)
    : ControllerBase
{
    [HttpGet]
    [Route("Config")]
    public string? Get(string key)
    {
        return config[key];
    }

    [HttpGet]
    [Route("ConnStr")]
    public string? GetConnStr(string key)
    {
        return config.GetConnectionString(key);
    }
}