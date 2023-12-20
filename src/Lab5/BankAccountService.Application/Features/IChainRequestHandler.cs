namespace BankAccountService.Application.Features;

public interface IChainRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
{
    public IRequestHandler<TRequest, TResponse>? Successor { get; set; }
}