namespace Domain.DTOs.StudentDTOs;

public class CreateStudentDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
}
