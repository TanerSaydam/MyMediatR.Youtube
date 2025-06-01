using TS.MediatR;

namespace eTicaret.Application;
public sealed record ProductCreateCommand(
    string Name,
    decimal Price) : IRequest;

internal sealed class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand>
{
    public async Task Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        //db kayıt işlemi...

        await Task.CompletedTask;
    }
}