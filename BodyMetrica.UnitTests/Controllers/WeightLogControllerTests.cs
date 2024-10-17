using BodyMetrica.Api.Controllers;
using BodyMetrica.Api.Models.Weight;
using BodyMetrica.Domain.Weight;
using BodyMetrica.Domain.Weight.Requests;
using BodyMetrica.Domain.Weight.Services;
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
        mediator.Setup(x => x.Send(It.IsAny<AddNewLogWeightRequest>(), default)).ReturnsAsync(Result.Ok());
        var userService = new Mock<IUserService>();
        var ctrl = new WeightLogController(mediator.Object, userService.Object);
        var result = await ctrl.Post(new LogWeightRequest { Weight = 70.234m, RecordDate = DateTimeOffset.Now });
        Assert.IsType<OkResult>(result);
    }
}