using Autofac;
using BodyMetrica.Domain.Common.Repositories;
using BodyMetrica.Domain.Weight.Repositories;
using BodyMetrica.Domain.Weight.Services;
using BodyMetrica.Infrastructure;
using BodyMetrica.Infrastructure.DataAccess;
using BodyMetrica.Infrastructure.DataAccess.Weight;
using Module = Autofac.Module;

namespace BodyMetrica.Api.Bootstrap;

public class ManualDependencyInjectionModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        RegisterServices(builder);
        RegisterRepositories(builder);
    }

    private static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<UserProfileService>().As<IUserProfileService>();
    }

    private static void RegisterRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<WeightLogRepository>().As<IWeightLogRepository>();
        builder.RegisterType<UserRepository>().As<IUserRepository>();
    }
}