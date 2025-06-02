using TS.MediatR;

namespace eTicaret.Application.Behaviors;

public sealed class ValidationBehavior<TRequest> : IPipelineBehavior<TRequest>
    where TRequest : IRequest
{
    public async Task Handle(TRequest request, RequesteHandlerDelete next, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Validation işlem başladı...");

        await next();

        Console.WriteLine("Validation İşlem sonu");
    }
}
