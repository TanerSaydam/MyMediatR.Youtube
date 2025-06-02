using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TS.MediatR;

public static class ServiceRegistrar
{
    public static IServiceCollection AddMediatR(
        this IServiceCollection services,
        Assembly assembly)
    {
        var types = assembly.GetTypes().Where(t => !t.IsInterface && !t.IsAbstract);

        var handlerTypes = types.SelectMany(t => t
            .GetInterfaces()
            .Where(i => i.IsGenericType && (
            i.GetGenericTypeDefinition() == typeof(IRequestHandler<>)
            || i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)
            ))
            .Select(s => new { Interface = s, Impletation = t })
            );

        services.AddTransient<ISender, Sender>();

        foreach (var item in handlerTypes)
        {
            services.AddTransient(item.Interface, item.Impletation);
        }

        return services;
    }
}