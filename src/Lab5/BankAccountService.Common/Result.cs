using BankAccountService.Common.Interfaces;

namespace BankAccountService.Common;

public class Result<TData> : IResult<TData>
{
    public Result() { }

    public Result(
        IReadOnlyList<string>? messages,
        bool succeeded,
        TData? data)
    {
        ArgumentNullException.ThrowIfNull(messages);

        Messages = messages;
        Succeeded = succeeded;
        Data = data;
    }

    public IReadOnlyList<string> Messages { get; set; } = new List<string>();
    public bool Succeeded { get; set; }
    public TData? Data { get; set; }
}