using BodyMetrica.Contracts.Weight.Requests;
using BodyMetrica.Domain.Common.Models;
using BodyMetrica.Domain.Common.Services;
using BodyMetrica.Domain.Weight.Entities;
using BodyMetrica.Domain.Weight.Handlers;
using BodyMetrica.Domain.Weight.Repositories;
using FluentResults;
using Moq;

namespace BodyMetrica.UnitTests.Commands;

public class AddNewWeightLogRequestTests
{
    [Fact]
    public async Task Should_Add_New_Weight()
    {
        var cmd = new AddNewWeightLogRequest();
        var weightRepository = new Mock<IWeightLogRepository>();
        weightRepository.Setup(x => x.AddNew(It.IsAny<WeightLog>()))
            .ReturnsAsync(Result.Ok());
        var userProfile = new Mock<IUserService>();
        userProfile.Setup(x => x.GetCurrentUser())
            .Returns(new User { Id = 1 });
        var cmdHandler = new AddNewWeightLogRequestHandler(weightRepository.Object, userProfile.Object);
        var result = await cmdHandler.Handle(cmd, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}