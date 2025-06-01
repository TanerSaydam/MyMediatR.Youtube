using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TS.MediatR;
public interface IRequest { }
public interface IRequest<TResponse> { }

public interface IRequestHandler<TRequest> where TRequest : IRequest
{
    Task Handle(TRequest request, CancellationToken cancellationToken);
}
public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public interface ISender
{
    Task Send(IRequest request, CancellationToken cancellationToken = default);
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}

public sealed class Sender(
    IServiceProvider serviceProvider) : ISender
{
    public async Task Send(IRequest request, CancellationToken cancellationToken = default)
    {
        var type = typeof(IRequestHandler<>).MakeGenericType(request.GetType());

        using (var scoped = serviceProvider.CreateScope())
        {
            var handler = scoped.ServiceProvider.GetRequiredService(type);
            await (Task)type.GetMethod("Handle")!
                .Invoke(handler, new object[] { request, cancellationToken })!;
        }
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var type = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

        using (var scoped = serviceProvider.CreateScope())
        {
            var handler = scoped.ServiceProvider.GetRequiredService(type);
            var res = await (Task<TResponse>)type.GetMethod("Handle")!
                .Invoke(handler, new object[] { request, cancellationToken })!;

            return res;
        }
    }
}

public static class MediatRExtensions
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