using BodyMetrica.Api.Controllers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace BodyMetrica.Api.Bootstrap;

public static class WebAppConfiguration
{
    public static void ConfigureWebApp(this WebApplication app )
    {

        app.MapHealthChecks("/healthcheck", new HealthCheckOptions()
        {
            ResponseWriter = HealthCheck.WriteResponse
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Migrate();
    }
}