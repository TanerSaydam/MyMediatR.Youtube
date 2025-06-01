using Microsoft.Extensions.DependencyInjection;
using TS.MediatR;

namespace eTicaret.Application;
public static class ServiceRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<ProductCreateCommand>, ProductCreateCommandHandler>();
        //services.AddMediatR(cfr =>
        //{
        //    cfr.RegisterServicesFromAssembly(typeof(ServiceRegistrar).Assembly);
        //});

        return services;
    }
}