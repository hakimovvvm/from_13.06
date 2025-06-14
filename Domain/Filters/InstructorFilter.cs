namespace Domain.Filters;

public class InstructorFilter : ValidFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
}
