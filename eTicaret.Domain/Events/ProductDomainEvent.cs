using TS.MediatR;

namespace eTicaret.Domain.Events;
public sealed class ProductDomainEvent : INotification
{
    public Guid Id { get; set; }
    public ProductDomainEvent(Guid id)
    {
        Id = id;
    }
}

public sealed class ProductSendEmailDomainEventHandler : INotificationHandler<ProductDomainEvent>
{
    public Task Handle(ProductDomainEvent notification, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Mail gönderme işlemine başladı...");
        return Task.CompletedTask;
    }
}