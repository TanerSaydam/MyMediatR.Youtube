using eTicaret.Application.Behaviors;
using eTicaret.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using TS.MediatR;

namespace eTicaret.Application;
public static class ServiceRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configure =>
        {
            configure.AddRegisterAssemblies(typeof(ServiceRegistrar).Assembly);
            configure.AddOpenBehavior(typeof(LogBehavior<>));
            configure.AddOpenBehavior(typeof(LogBehavior<,>));
            configure.AddOpenBehavior(typeof(ValidationBehavior<>));
        });

        services.AddTransient(
            typeof(INotificationHandler<ProductDomainEvent>), typeof(ProductSendEmailDomainEventHandler));

        return services;
    }
}