namespace Domain.DTOs.CourseDTOs;

public class CourseDTO
{
    public int CourseId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
