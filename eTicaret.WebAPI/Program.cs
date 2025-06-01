using eTicaret.Application;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/products",
    async (ProductCreateCommand request, ISender sender, CancellationToken cancellationToken) =>
    {
        await sender.Send(request, cancellationToken);

        return new { Message = "Create product is successful" };
    });

app.Run();