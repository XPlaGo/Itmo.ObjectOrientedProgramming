namespace IdentityService.Application.Features;

public interface IRequestHandler<in TRequest, TResult>
{
    public Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}