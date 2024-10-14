using BodyMetrica.Api.Controllers;
using BodyMetrica.Api.Models.Weight;
using BodyMetrica.Domain.Weight;
using Microsoft.AspNetCore.Mvc;

namespace BodyMetrica.UnitTests.Controllers;

public class WeightControllerTests
{
    [Fact]
    public void Should_Add_New_Weight()
    {
        var ctrl = new WeightController(null,null);
        var result = ctrl.Post(new AddWeightRequest { Weight = 70.234m, Units = WeightUnits.Kg, Date = DateTimeOffset.Now });
        Assert.IsType<OkResult>(result);
    }
}