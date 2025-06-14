namespace Domain.ApiResponse;

public class PagedResponse<T> : Response<T>
{
    public int TotalDates { get; set; }
    public int TotalPages { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PagedResponse(T data, int totalDates, int pageNumber, int pageSize) : base(data)
    {
        PageNumber = pageNumber <= 1 ? 1 : pageNumber;
        PageSize = pageSize <= 1 ? 1 : pageSize;
        TotalDates = totalDates;
        TotalPages = (int)Math.Ceiling((double)totalDates / pageSize);
    }
}
