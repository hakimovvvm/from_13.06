namespace Domain.DTOs.EnrollmentDTOs;

public class EnrollmentDTO
{
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrollDate { get; set; }
    public decimal? Grade { get; set; }
}
