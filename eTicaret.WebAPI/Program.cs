using eTicaret.Application;
using TS.MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/products",
    async (ProductCreateCommand request, IRequestHandler<ProductCreateCommand> requestHandler, CancellationToken cancellationToken) =>
    {
        await requestHandler.Handle(request, cancellationToken);
        //await sender.Send(request, cancellationToken);

        return new { Message = "Create product is successful" };
    });

app.Run();