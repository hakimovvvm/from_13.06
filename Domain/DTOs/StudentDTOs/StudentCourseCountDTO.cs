namespace Domain.DTOs.StudentDTOs;

public class StudentCourseCountDTO
{
    public int StudentId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int CourseCount { get; set; }
}
