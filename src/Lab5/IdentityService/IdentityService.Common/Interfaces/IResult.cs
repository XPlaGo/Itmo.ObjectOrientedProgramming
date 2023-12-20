namespace IdentityService.Common.Interfaces;

public interface IResult<TData>
{
    public IReadOnlyList<string> Messages { get; set; }
    public bool Succeeded { get; set; }
    public TData? Data { get; set; }
    public Exception? Exception { get; set; }
    int Code { get; set; }
}