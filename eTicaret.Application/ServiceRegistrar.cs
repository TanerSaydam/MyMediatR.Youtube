using eTicaret.Application.Behaviors;
using eTicaret.Domain;
using Microsoft.Extensions.DependencyInjection;
using TS.MediatR;

namespace eTicaret.Application;
public static class ServiceRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configure =>
        {
            configure.AddRegisterAssemblies(typeof(ServiceRegistrar).Assembly, typeof(Product).Assembly);
            configure.AddOpenBehavior(typeof(LogBehavior<>));
            configure.AddOpenBehavior(typeof(LogBehavior<,>));
            configure.AddOpenBehavior(typeof(ValidationBehavior<>));
        });

        return services;
    }
}