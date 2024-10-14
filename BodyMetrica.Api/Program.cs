using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;
using BodyMetrica.Domain.Weight;
using BodyMetrica.Domain.Weight.Repositories;
using BodyMetrica.Infrastructure.DataAccess;
using BodyMetrica.Infrastructure.DataAccess.Weight;

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


var connectionString = builder.Configuration.GetConnectionString("SQLAZURECONNSTR_BodyMetrica");
connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_BodyMetrica");
builder.Services.AddSqlServer<BodyMetricaDbContext>(connectionString);

builder.Services.AddTransient<IWeightRepository, WeightRepository>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<AddWeightCommand>();
});

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

app.Run();