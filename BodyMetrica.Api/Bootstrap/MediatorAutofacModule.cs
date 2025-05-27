using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using Module = Autofac.Module;

namespace BodyMetrica.Api.Bootstrap;

public class MediatorAutofacModule : Module
{
    private readonly IEnumerable<Assembly> _assembliesToScan;

    public MediatorAutofacModule(IEnumerable<Assembly> assembliesToScan)
    {
        _assembliesToScan = assembliesToScan;
    }

    public MediatorAutofacModule(params Assembly[] assembliesToScan) : this((IEnumerable<Assembly>)assembliesToScan)
    {
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder
            .RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

        // register DI for mediatr types
        var mediatrOpenTypes = new[]
        {
            typeof(IRequestHandler<>),
            typeof(IRequestHandler<,>),
            typeof(IRequestExceptionHandler<,,>),
            typeof(IRequestExceptionAction<,>),
            typeof(INotificationHandler<>),
            typeof(IStreamRequestHandler<,>)
        };

        foreach (var mediatrOpenType in mediatrOpenTypes)
            builder
                .RegisterAssemblyTypes(_assembliesToScan.ToArray())
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
    }
}