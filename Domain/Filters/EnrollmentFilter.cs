namespace Domain.Filters;

public class EnrollmentFilter : ValidFilter
{
    public int? StudentId { get; set; }
    public int? CourseId { get; set; }
    public decimal? MinGrade { get; set; }
    public decimal? MaxGrade { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
