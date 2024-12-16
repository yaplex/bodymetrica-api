using Autofac;
using BodyMetrica.Features.Common;
using Module = Autofac.Module;

namespace BodyMetrica.Api.Bootstrap;

public class ManualDependencyInjectionModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        RegisterServices(builder);
    }

    private static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<UserService>().As<IUserService>();
        builder.RegisterType<Auth0>().As<IAuth0>();
    }

}