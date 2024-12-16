using BodyMetrica.Api.Controllers;
using BodyMetrica.Features.Weight.AddNewWeightLog;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BodyMetrica.UnitTests.Controllers;

public class WeightLogControllerTests
{
    [Fact]
    public async Task Should_Add_New_Weight()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(x => x.Send(It.IsAny<AddNewWeightLogRequest>(), default)).ReturnsAsync(Result.Ok());
        var ctrl = new WeightLogController(mediator.Object);
        var result = await ctrl.Post(new AddNewWeightLogRequest { Weight = 70.234m, RecordDate = DateTimeOffset.Now });
        Assert.IsType<OkResult>(result);
    }
}