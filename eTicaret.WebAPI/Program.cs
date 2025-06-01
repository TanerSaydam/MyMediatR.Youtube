using eTicaret.Application;
using TS.MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddTransient<Test>();
var app = builder.Build();

app.MapGet("/", (IServiceProvider serviceProvider) =>
{
    var type = typeof(Test);
    var handler = serviceProvider.GetRequiredService(type);
    var res = type.GetMethod("Calculate")!.Invoke(handler, null);
    return res;
});

app.MapPost("/products",
    async (ProductCreateCommand request, ISender sender, CancellationToken cancellationToken) =>
    {
        await sender.Send(request, cancellationToken);

        return new { Message = "Create product is successful" };
    });

app.Run();

class Test
{
    public int Calculate()
    {
        return 5054646;
    }
}