using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BodyMetrica.Api.Controllers;
using BodyMetrica.Domain.Common.Models;
using BodyMetrica.Domain.Weight.Features.AddNewWeightLog;
using BodyMetrica.Domain.Weight.Repositories;
using BodyMetrica.Infrastructure.DataAccess;
using BodyMetrica.Infrastructure.DataAccess.Migrations;
using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;

namespace BodyMetrica.Api.Bootstrap;

public static class DependencyRegistration
{
    public static void RegisterDependencies(this WebApplicationBuilder builder)
    {
        var assembliesToScan = new List<Assembly>
        {
            typeof(WeightLogController).Assembly,
            typeof(BodyMetricaDbContext).Assembly,
            typeof(IWeightLogRepository).Assembly,
            typeof(ValueObject).Assembly
        };


        LoggingDependencies(builder);
        DatabaseDependencies(builder);
        RegisterAutofac(builder, assembliesToScan);

        RequireAuthenticationForAllApiEndpoint(builder);
        ConfigureAuth0(builder);
        AddCorsForEndpoints(builder);

        AddFluentValidation(builder);

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAutoMapper(assembliesToScan);
    }

    private static void AddCorsForEndpoints(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(x =>
            x.AddDefaultPolicy(p =>
            {
                p.AllowAnyHeader();
                p.AllowAnyOrigin();
                p.AllowAnyMethod();
            }));
    }

    private static void ConfigureAuth0(WebApplicationBuilder builder)
    {
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
    }

    private static void RequireAuthenticationForAllApiEndpoint(WebApplicationBuilder builder)
    {
        builder.Services.AddMvc(o =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            o.Filters.Add(new AuthorizeFilter(policy));
        });
    }


    private static void AddFluentValidation(WebApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(AddNewLogWeightRequestValidator));
    }

    private static void LoggingDependencies(WebApplicationBuilder builder)
    {
        builder.Services.AddSerilog((services, configuration) =>
            configuration.ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext());
    }

    private static void RegisterAutofac(WebApplicationBuilder builder, List<Assembly> assembliesToScan)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterModule(new MediatorAutofacModule(assembliesToScan));
            containerBuilder.RegisterModule(new ManualDependencyInjectionModule());
        });
    }

    private static void DatabaseDependencies(WebApplicationBuilder builder)
    {
        var connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_BodyMetrica");
        builder.Services.AddSqlServer<BodyMetricaDbContext>(connectionString);
        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Initialize).Assembly).For.Migrations()
            );
    }

    public static IApplicationBuilder Migrate(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
        return app;
    }
}