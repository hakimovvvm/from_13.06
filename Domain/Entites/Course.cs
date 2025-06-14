using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Course
{
    public int CourseId { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public List<Enrollment> Enrollments { get; set; }

    public List<CourseAssignment> CourseAssignments { get; set; } 
}
