using Microsoft.Extensions.DependencyInjection;

namespace TS.MediatR;
public interface IRequest { }

public interface IRequestHandler<TRequest> where TRequest : IRequest
{
    Task Handle(TRequest request, CancellationToken cancellationToken);
}

public interface ISender
{
    Task Send(IRequest request, CancellationToken cancellationToken = default);
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
        Console.WriteLine();
    }
}