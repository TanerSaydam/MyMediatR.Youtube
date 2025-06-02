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
    public async Task Handle(ProductDomainEvent notification, CancellationToken cancellationToken = default)
    {
        await Task.Delay(1000);
        Console.WriteLine("Mail gönderme işlemine başladı: " + DateTime.Now);
    }
}

//public sealed class ProductSendSmsDomainEventHandler : INotificationHandler<ProductDomainEvent>
//{
//    public async Task Handle(ProductDomainEvent notification, CancellationToken cancellationToken = default)
//    {
//        await Task.Delay(1000);
//        Console.WriteLine("Sms gönderme işlemine başladı: " + DateTime.Now);
//    }
//}