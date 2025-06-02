namespace TS.MediatR;
public interface IRequest { }
public interface IRequest<TResponse> { }

public interface IRequestHandler<TRequest> where TRequest : IRequest
{
    Task Handle(TRequest request, CancellationToken cancellationToken);
}
public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public delegate Task RequesteHandlerDelete();
public delegate Task<TResponse> RequesteHandlerDelete<TResponse>();