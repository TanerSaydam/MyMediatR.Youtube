using TS.MediatR;

namespace eTicaret.Application.Behaviors;
public sealed class LogBehavior<TRequest> : IPipelineBehavior<TRequest>
    where TRequest : IRequest
{
    public async Task Handle(TRequest request, RequesteHandlerDelete next, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Log işlem başladı...");

        await next();

        Console.WriteLine("Log işlem sonu");
    }
}

public sealed class LogBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequesteHandlerDelete<TResponse> next, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Log işlem başladı...");

        var res = await next();

        Console.WriteLine("Log işlem sonu");

        return res;
    }
}
