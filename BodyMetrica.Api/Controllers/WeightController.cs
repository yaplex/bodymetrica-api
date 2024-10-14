using System.Security.Claims;
using BodyMetrica.Api.Models.Weight;
using BodyMetrica.Domain.Weight;
using BodyMetrica.Infrastructure.DataAccess.Weight;
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

        [HttpGet]
        public async Task<IEnumerable<Weight>> Get()
        {
            var query = new GetWeightsQuery();
            IEnumerable<Weight> result = await mediator.Send(query);
            return result;
        }
    }
}
