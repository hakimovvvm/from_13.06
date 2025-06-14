using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Instructor
{
    public int InstructorId { get; set; }

    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    public ICollection<CourseAssignment> CourseAssignments { get; set; }
}
