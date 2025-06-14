namespace Domain.Entites;

public class CourseAssignment
{
    public int CourseAssignmentId { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; } = null!;
}
