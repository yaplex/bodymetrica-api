using BodyMetrica.Domain.Weight;
using BodyMetrica.Domain.Weight.Repositories;
using FluentResults;
using Moq;

namespace BodyMetrica.UnitTests.Commands;

public class AddWeightCommandTests
{
    [Fact]
    public async Task Should_Add_New_Weight()
    {
        var cmd = new AddWeightCommand(70.234, WeightUnits.Kg, DateTimeOffset.Now);
        var weightRepository = new Moq.Mock<IWeightRepository>();
        weightRepository.Setup(x => x.AddNew(It.IsAny<Weight>(), It.IsAny<DateTimeOffset>()))
            .ReturnsAsync(Result.Ok());
        var cmdHandler = new AddWeightCommandHandler(weightRepository.Object);
        var result = await cmdHandler.Handle(cmd, CancellationToken.None);

        Assert.True(result.IsSuccess);

    }
}