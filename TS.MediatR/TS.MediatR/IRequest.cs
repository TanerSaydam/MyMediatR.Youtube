namespace TS.MediatR;
public interface IRequest { }

public interface IRequestHandler<TRequest> where TRequest : IRequest
{
    Task Handle(TRequest request, CancellationToken cancellationToken);
}