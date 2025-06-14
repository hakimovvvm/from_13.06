namespace Domain.DTOs.CurseDTOs;

public class CreateCourseDTO
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
