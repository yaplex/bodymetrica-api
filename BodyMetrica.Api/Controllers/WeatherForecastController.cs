using System.Security.Claims;
using BodyMetrica.Api.Models.Weight;
using BodyMetrica.Domain.Weight;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BodyMetrica.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeightController(IMediator mediator, ILogger<WeightController> logger) : ControllerBase
    {
        private readonly ILogger<WeightController> _logger = logger;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddWeightRequest request)
        {
            var cmd = new AddWeightCommand(request.Weight, request.Units, request.Date);
            var result = await mediator.Send(cmd);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public object Get()
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            var claims = user.Claims.GroupBy(x => x.Type).ToDictionary(x => x.Key, x => x.First().Value);

            var apiData=  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
            })
            .ToArray();

            return new {ApiData = apiData, Claims = claims};
        }
    }
}
