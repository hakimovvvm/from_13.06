namespace Domain.Filters;

public class CourseFilter : ValidFilter
{
    public string? Title { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
