using eTicaret.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using TS.MediatR;

namespace eTicaret.Application;
public static class ServiceRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddMediatR(typeof(ServiceRegistrar).Assembly);
        //services.AddTransient(typeof(IPipelineBehavior<>), typeof(LogBehavior<>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<>), typeof(ValidationBehavior<>));

        services.AddMediatR(configure =>
        {
            configure.AddRegisterAssemblies(typeof(ServiceRegistrar).Assembly);
            configure.AddOpenBehavior(typeof(LogBehavior<>));
            configure.AddOpenBehavior(typeof(LogBehavior<,>));
            configure.AddOpenBehavior(typeof(ValidationBehavior<>));
        });
        return services;
    }
}