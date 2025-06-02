using Microsoft.Extensions.DependencyInjection;

namespace TS.MediatR;

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
        using var scoped = serviceProvider.CreateScope();

        RequesteHandlerDelete handlerDelete = () =>
        {
            var interfaceType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
            var handler = scoped.ServiceProvider.GetRequiredService(interfaceType);
            var method = interfaceType.GetMethod("Handle")!;
            return (Task)method.Invoke(handler, new object[] { request, cancellationToken })!;
        };

        await handlerDelete();
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        using var scoped = serviceProvider.CreateScope();

        RequesteHandlerDelete<TResponse> handlerDelete = () =>
        {
            var interfaceType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = scoped.ServiceProvider.GetRequiredService(interfaceType);
            var method = interfaceType.GetMethod("Handle")!;
            return (Task<TResponse>)method.Invoke(handler, new object[] { request, cancellationToken })!;
        };

        return await handlerDelete();
    }
}
