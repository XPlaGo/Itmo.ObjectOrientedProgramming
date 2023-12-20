namespace IdentityService.Common;

public class PaginatedResult<T> : Result<T>
{
    public PaginatedResult(IReadOnlyList<T> data)
    {
        Data = data;
    }

    public PaginatedResult(bool succeeded, IReadOnlyList<T>? data = default, IReadOnlyList<string>? messages = null, int count = 0, int pageNumber = 1, int pageSize = 10)
    {
        ArgumentNullException.ThrowIfNull(messages);

        Data = data;
        CurrentPage = pageNumber;
        Succeeded = succeeded;
        Messages = messages;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
    }

    public PaginatedResult(IReadOnlyList<T> data, int count, int pageNumber, int pageSize)
        : this(true, data, null, count, pageNumber, pageSize) { }

    public new IReadOnlyList<T>? Data { get; set; }

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}