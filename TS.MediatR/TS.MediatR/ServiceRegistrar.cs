using Microsoft.Extensions.DependencyInjection;

namespace TS.MediatR;

public static class ServiceRegistrar
{
    public static IServiceCollection AddMediatR(
        this IServiceCollection services,
        Action<MediatROptions> options)
    {
        if (options is null) throw new ArgumentNullException(nameof(options));

        var config = new MediatROptions();
        options(config);

        foreach (var assembly in config.Assemblies)
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
        }

        foreach (var pipeline in config.PipelineBehaviors)
        {
            var genericArg = pipeline.GetGenericArguments().Length;

            if (genericArg == 1)
            {
                services.AddTransient(typeof(IPipelineBehavior<>), pipeline);
            }
            else if (genericArg == 2)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), pipeline);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(genericArg));
            }
        }

        return services;
    }
}