namespace TransactionService.Common.Interfaces;

public interface IResultFactory
{
    public Result<T> Success<T>();

    public Result<T> Success<T>(string message);

    public Result<T> Success<T>(T data);

    public Result<T> Success<T>(T data, string message);

    public Result<T> Failure<T>();

    public Result<T> Failure<T>(string message);

    public Result<T> Failure<T>(IReadOnlyList<string> messages);

    public Result<T> Failure<T>(T data);

    public Result<T> Failure<T>(T data, string message);

    public Result<T> Failure<T>(T data, IReadOnlyList<string> messages);

    public Task<Result<T>> SuccessAsync<T>();

    public Task<Result<T>> SuccessAsync<T>(string message);

    public Task<Result<T>> SuccessAsync<T>(T data);

    public Task<Result<T>> SuccessAsync<T>(T data, string message);

    public Task<Result<T>> FailureAsync<T>();

    public Task<Result<T>> FailureAsync<T>(string message);

    public Task<Result<T>> FailureAsync<T>(IReadOnlyList<string> messages);

    public Task<Result<T>> FailureAsync<T>(T data);

    public Task<Result<T>> FailureAsync<T>(T data, string message);

    public Task<Result<T>> FailureAsync<T>(T data, IReadOnlyList<string> messages);
}