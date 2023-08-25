namespace IssuesApi.Domain.Inputs;

public class CreateProjectDTO
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}
