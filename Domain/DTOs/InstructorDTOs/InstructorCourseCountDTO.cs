namespace Domain.DTOs.InstructorDTOs;

public class InstructorCourseCountDTO
{
    public int InstructorId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int CourseCount { get; set; }
}
