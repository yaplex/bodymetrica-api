using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BodyMetrica.Api.Controllers;
using BodyMetrica.Features.BloodPressure.AddNewBloodPressureLog;
using BodyMetrica.Features.Persistence;
using BodyMetrica.Infrastructure.DataAccess.Migrations;
using FluentMigrator.Runner;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;

namespace BodyMetrica.Api.Bootstrap;

public static class BuilderConfiguration
{
    public static void ConfigureWebBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck<HealthCheck>("BasicHealthCheck");

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var assembliesToScan = new List<Assembly>
        {
            typeof(WeightLogController).Assembly,
            typeof(BodyMetricaDbContext).Assembly,
            typeof(LogNewBloodPressureCommandValidator).Assembly,
            typeof(IRecord).Assembly
        };


        LoggingDependencies(builder);
        DatabaseDependencies(builder);
        RegisterAutofac(builder, assembliesToScan);

        RequireAuthenticationForAllApiEndpoint(builder);
        ConfigureAuth0(builder);
        AddCorsForEndpoints(builder);

        AddFluentValidation(builder, assembliesToScan);

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAutoMapper(assembliesToScan);
        builder.Services.AddHttpClient();
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
            options.Authority = "https://auth.bodymetrica.com";
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


    private static void AddFluentValidation(WebApplicationBuilder builder, List<Assembly> assembliesToScan)
    {
        // builder.Services.AddFluentValidationAutoValidation();
        // builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblies(assembliesToScan);
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
        var connectionString = builder.Configuration.GetConnectionString("BodyMetrica");
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