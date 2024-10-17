using BodyMetrica.Domain.Common.Models;
using BodyMetrica.Domain.Weight;
using BodyMetrica.Domain.Weight.Persistence;
using BodyMetrica.Domain.Weight.Repositories;
using BodyMetrica.Domain.Weight.Requests;
using BodyMetrica.Domain.Weight.Services;
using FluentResults;
using Moq;

namespace BodyMetrica.UnitTests.Commands;

public class AddNewLogWeightRequestTests
{
    [Fact]
    public async Task Should_Add_New_Weight()
    {
        var cmd = new AddNewLogWeightRequest(70.234m, DateTimeOffset.Now);
        var weightRepository = new Mock<IWeightLogRepository>();
        weightRepository.Setup(x => x.AddNew(It.IsAny<WeightLogRecord>()))
            .ReturnsAsync(Result.Ok());
        var userProfile = new Mock<IUserProfileService>();
        userProfile.Setup(x => x.GetUserProfile())
            .Returns(new UserProfile { Id = 1, WeightUnits = "kg" });
        var cmdHandler = new AddWeightRequestHandler(weightRepository.Object, userProfile.Object);
        var result = await cmdHandler.Handle(cmd, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}