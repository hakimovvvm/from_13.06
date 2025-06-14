namespace Domain.Filters;

public class CourseAssignmentFilter : ValidFilter
{
    public int? CourseId { get; set; }
    public int? InstructorId { get; set; }
}
