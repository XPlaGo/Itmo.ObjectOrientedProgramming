using TransactionService.Common.Interfaces;

namespace TransactionService.Common.Factories;

public class ResultFactory : IResultFactory
{
    public Result<T> Success<T>()
    {
        return new Result<T>
        {
            Succeeded = true,
        };
    }

    public Result<T> Success<T>(string message)
    {
        return new Result<T>
        {
            Succeeded = true,
            Messages = new List<string> { message },
        };
    }

    public Result<T> Success<T>(T data)
    {
        return new Result<T>
        {
            Succeeded = true,
            Data = data,
        };
    }

    public Result<T> Success<T>(T data, string message)
    {
        return new Result<T>
        {
            Succeeded = true,
            Messages = new List<string> { message },
            Data = data,
        };
    }

    public Result<T> Failure<T>()
    {
        return new Result<T>
        {
            Succeeded = false,
        };
    }

    public Result<T> Failure<T>(string message)
    {
        return new Result<T>
        {
            Succeeded = false,
            Messages = new List<string> { message },
        };
    }

    public Result<T> Failure<T>(IReadOnlyList<string> messages)
    {
        return new Result<T>
        {
            Succeeded = false,
            Messages = messages,
        };
    }

    public Result<T> Failure<T>(T data)
    {
        return new Result<T>
        {
            Succeeded = false,
            Data = data,
        };
    }

    public Result<T> Failure<T>(T data, string message)
    {
        return new Result<T>
        {
            Succeeded = false,
            Messages = new List<string> { message },
            Data = data,
        };
    }

    public Result<T> Failure<T>(T data, IReadOnlyList<string> messages)
    {
        return new Result<T>
        {
            Succeeded = false,
            Messages = messages,
            Data = data,
        };
    }

    public Task<Result<T>> SuccessAsync<T>()
    {
        return Task.FromResult(Success<T>());
    }

    public Task<Result<T>> SuccessAsync<T>(string message)
    {
        return Task.FromResult(Success<T>(message));
    }

    public Task<Result<T>> SuccessAsync<T>(T data)
    {
        return Task.FromResult(Success(data));
    }

    public Task<Result<T>> SuccessAsync<T>(T data, string message)
    {
        return Task.FromResult(Success(data, message));
    }

    public Task<Result<T>> FailureAsync<T>()
    {
        return Task.FromResult(Failure<T>());
    }

    public Task<Result<T>> FailureAsync<T>(string message)
    {
        return Task.FromResult(Failure<T>(message));
    }

    public Task<Result<T>> FailureAsync<T>(IReadOnlyList<string> messages)
    {
        return Task.FromResult(Failure<T>(messages));
    }

    public Task<Result<T>> FailureAsync<T>(T data)
    {
        return Task.FromResult(Failure(data));
    }

    public Task<Result<T>> FailureAsync<T>(T data, string message)
    {
        return Task.FromResult(Failure(data, message));
    }

    public Task<Result<T>> FailureAsync<T>(T data, IReadOnlyList<string> messages)
    {
        return Task.FromResult(Failure(data, messages));
    }
}