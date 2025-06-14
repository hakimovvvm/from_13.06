namespace Domain.Entites;

public class Enrollment
{
    public int EnrollmentId { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public DateTime EnrollDate { get; set; }

    public decimal? Grade { get; set; }
}
