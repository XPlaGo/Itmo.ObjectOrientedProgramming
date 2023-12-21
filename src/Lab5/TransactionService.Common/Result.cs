using TransactionService.Common.Interfaces;

namespace TransactionService.Common;

public class Result<TData> : IResult<TData>
{
    public IReadOnlyList<string> Messages { get; set; } = new List<string>();
    public bool Succeeded { get; set; }
    public TData? Data { get; set; }
}