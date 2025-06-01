using TS.MediatR;

namespace eTicaret.Application;
public sealed record ProductGetAllQuery : IRequest<List<int>>;

internal sealed class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, List<int>>
{
    public Task<List<int>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
    {
        var res = new List<int>()
        {
            1,2,3,4,5,6,7,
        };

        return Task.FromResult(res);
    }
}