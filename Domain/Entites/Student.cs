using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Student
{
    public int StudentId { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public List<Enrollment> Enrollments { get; set; }
}
