using eTicaret.Domain;
using eTicaret.Domain.Events;
using TS.MediatR;

namespace eTicaret.Application;
public sealed record ProductCreateCommand(
    string Name,
    decimal Price) : IRequest;

internal sealed class ProductCreateCommandHandler(
    ISender sender) : IRequestHandler<ProductCreateCommand>
{
    public async Task Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        //db kayıt işlemi...

        Product product = new()
        {
            Name = request.Name,
            Price = request.Price,
        };

        //Mail Gönder
        await sender.Publish(new ProductDomainEvent(product.Id));
        await Task.CompletedTask;
    }
}