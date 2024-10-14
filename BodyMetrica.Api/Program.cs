using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;
using BodyMetrica.Domain.Weight;
using BodyMetrica.Domain.Weight.Repositories;
using BodyMetrica.Infrastructure.DataAccess;
using BodyMetrica.Infrastructure.DataAccess.Weight;
using FluentMigrator.Runner;
using BodyMetrica.Infrastructure.DataAccess.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(o =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    o.Filters.Add(new AuthorizeFilter(policy));
});

// 1. Add Authentication Services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://bodymetrica.us.auth0.com/";
    options.Audience = "https://api.bodymetrica.com";
    options.MapInboundClaims = false;
});

builder.Services.AddCors(x =>
    x.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyOrigin();
        p.AllowAnyMethod();
    }));


var connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_BodyMetrica");
builder.Services.AddSqlServer<BodyMetricaDbContext>(connectionString);

builder.Services.AddTransient<IWeightRepository, WeightRepository>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<AddWeightCommand>();
});

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddSqlServer()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(Initialize).Assembly).For.Migrations()
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
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

app.Run();


public static class MigrationExtension
{
    public static IApplicationBuilder Migrate(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
        return app;
    }
}