namespace Domain.Filters;

public class StudentFilter : ValidFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
}
