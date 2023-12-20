using IdentityService.Common.Interfaces;

namespace IdentityService.Common;

public class Result<TData> : IResult<TData>
{
    public Result() { }

    public Result(
        IReadOnlyList<string>? messages,
        bool succeeded,
        TData? data,
        Exception? exception = null,
        int code = 0)
    {
        ArgumentNullException.ThrowIfNull(messages);

        Messages = messages;
        Succeeded = succeeded;
        Data = data;
        Exception = exception;
        Code = code;
    }

    public IReadOnlyList<string> Messages { get; set; } = new List<string>();
    public bool Succeeded { get; set; }
    public TData? Data { get; set; }
    public Exception? Exception { get; set; }
    public int Code { get; set; }
}